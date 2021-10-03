using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public Queue<Profile> Profiles { get; set; }
    public Queue<Evidence> Evidence { get; set; }

    public GameObject InventoryUI;

    void Start()
    {
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (!InventoryUI.activeSelf)
            {
                InventoryUI.SetActive(true);
                InventoryUI.GetComponent<Animation>().Play("Slide In");
            }
            else
            {
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
}
