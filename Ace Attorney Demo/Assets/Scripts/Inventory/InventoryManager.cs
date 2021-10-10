using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public List<Profile> Profiles;
    public List<Evidence> Evidence;

    public GameObject InventoryUI;
    public GameObject InventoryUIItems;

    public Text InventoryUITitle;
    public Image InventoryUIImage;
    public Text InventoryUIDescription;

    public Button PresentButton;

    public List<Button> ItemFrames;
    public List<Image> ItemImages;

    private List<Clue> Items;

    public bool isMenuActive = false;

    private Clue selected;

    void Start()
    {
        Items = new List<Clue>();

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
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (!InventoryUI.activeSelf)
            {
                isMenuActive = true;
                InventoryUI.SetActive(true);
                InventoryUI.GetComponent<Animation>().Play("Slide In");
            }
            else
            {
                isMenuActive = false;
                InventoryUI.GetComponent<Animation>().Play("Slide Out");
                StartCoroutine(DisableUI(InventoryUI.GetComponent<Animation>().GetClip("Slide Out").length));
            }
        }
    }

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
        }
    }

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

    private void SetDetailsInUI(Clue item)
    {
        InventoryUITitle.text = item.name;

        var setColor = InventoryUIImage.color;
        setColor.a = 1f;
        InventoryUIImage.color = setColor;

        InventoryUIImage.sprite = item.Image;

        InventoryUIDescription.text = item.Description;

        PresentButton.gameObject.SetActive(true);
    }

    public void PresentItem()
    {
        Debug.Log($"Present: { selected.name }");
    }
}
