using Ink.Runtime;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public List<Profile> Profiles;
    public List<Evidence> Evidence;

    // Move to InventoryUIHandler
    public GameObject InventoryUI;
    public GameObject InventoryUIItems;
    
    // Move to InventoryUIHandler
    public Text InventoryUITitle;
    public Image InventoryUIImage;
    public Text InventoryUIDescription;

    // Move to InventoryUIHandler
    public Button PresentButton;

    // Move to InventoryUIHandler
    public List<Button> ItemFrames;
    public List<Image> ItemImages;

    private List<Clue> Items;
    
    // Move to InventoryUIHandler
    public bool isMenuActive = false;

    private Clue selected;

    // See if this can be done through events
    public StoryManager storyManager;

    void Start()
    {
        Items = new List<Clue>();
        
        // Move to InventoryUIHandler
        foreach (Transform child in InventoryUIItems.transform)
        {
            ItemFrames.Add(child.GetComponentsInChildren<Button>()[0]);
            ItemImages.Add(child.GetComponentsInChildren<Image>()[1]);
        }

        foreach (Clue item in Evidence)
        {
            AddClueToCourtRecord(item);
        }

        SelectItem(0);
    }

    void Update()
    {
        // Maybe move to InputManager
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (!InventoryUI.activeSelf)
                ToggleInventory(true, false);
            else
                ToggleInventory(false, false);
        }
    }

    // Consider wether to make a C# event or use button click event directly
    public void OnCourtRecordButton()
    {
        if (isMenuActive)
            ToggleInventory(false, false);
        else
            ToggleInventory(true, false);
    }

    // Move to InventoryUIHandler
    public void ToggleInventory(bool enabled, bool fastHide)
    {
        isMenuActive = enabled;
        if (enabled)
        {
            InventoryUI.SetActive(enabled);
            InventoryUI.GetComponent<Animation>().Play("Slide In");
        }
        else
        {
            if (fastHide)
            {
                InventoryUI.SetActive(false);
            }
            else
            {
                InventoryUI.GetComponent<Animation>().Play("Slide Out");
                StartCoroutine(DisableUI(InventoryUI.GetComponent<Animation>().GetClip("Slide Out").length));
            }
        }
    }

    // Move to InventoryUIHandler
    IEnumerator DisableUI(float time)
    {
        yield return new WaitForSeconds(time);
        InventoryUI.SetActive(false);
    }
    
    public void AddClueToCourtRecord(Clue item)
    {
        if (item.GetType() == typeof(Evidence))
        {
            SetClueInUI(item);
        }
        else if(item.GetType() == typeof(Profile))
        {
            //TODO: Implement Profiles
        }
    }

    // Move to InventoryUIHandler
    private void SetClueInUI(Clue item)
    {
        Items.Add(item);
        Button[] itemFrames = ItemFrames.ToArray();
        Image[] itemImages = ItemImages.ToArray();
        Button itemFrame = itemFrames[Items.Count - 1];
        Image itemImage = itemImages[Items.Count - 1];

        var setColor = itemImage.color;
        setColor.a = 1f;
        itemImage.color = setColor;

        itemFrame.image.sprite = item.Image;
    }

    public void SelectItem(int index)
    {
        selected = Items[index];
        SetDetailsInUI(selected);
    }

    // Move to InventoryUIHandler
    private void SetDetailsInUI(Clue item)
    {
        InventoryUITitle.text = item.Name;

        var setColor = InventoryUIImage.color;
        setColor.a = 1f;
        InventoryUIImage.color = setColor;

        InventoryUIImage.sprite = item.Image;

        InventoryUIDescription.text = item.Description;

        PresentButton.gameObject.SetActive(true);
    }

    // Consider wether to make a C# event or use button click event directly
    public void PresentItem()
    {
        ToggleInventory(false, true);
        storyManager.PresentEvidence(selected);
    }
}
