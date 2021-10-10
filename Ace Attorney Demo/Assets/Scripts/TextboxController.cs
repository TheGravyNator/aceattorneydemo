using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextboxController : MonoBehaviour
{
    public string Name
    {
        get { return nameField.text; }
        set { nameField.text = value; }
    }
    public Text nameField;
    public Text textField;
    public GameObject ContinueDotImage;

    public bool canWrite = true;

    public void WriteText(string name, string text)
    {
        ContinueDotImage.SetActive(false);
        ContinueDotImage.GetComponent<Animation>().Stop();
        textField.text = "";
        nameField.text = name;
        gameObject.SetActive(true);
        canWrite = false;
        StartCoroutine(WriteTextbox(text));
    }

    IEnumerator WriteTextbox(string text)
    {

        foreach (char letter in text)
        {
            textField.text += letter;
            if(Input.GetKey(KeyCode.LeftShift) || Input.GetMouseButton(0)) yield return new WaitForSeconds(.01f);
            else yield return new WaitForSeconds(.05f);
        }
        canWrite = true;
        ContinueDotImage.SetActive(true);
        ContinueDotImage.GetComponent<Animation>().Play();
        yield return WaitForKeyPress();
    }

    IEnumerator WaitForKeyPress()
    {
        bool done = false;
        while (!done)
        {
            if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetMouseButtonDown(0))
            {
                done = true;
            }
            yield return null;
        }
    }
}
