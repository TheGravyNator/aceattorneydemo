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

    public bool canWrite = true;

    public void WriteText(string name, string text)
    {
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
            if(Input.GetButton("Jump")) yield return new WaitForSeconds(.01f);
            else yield return new WaitForSeconds(.05f);
        }
        canWrite = true;
        yield return WaitForKeyPress();
    }

    IEnumerator WaitForKeyPress()
    {
        bool done = false;
        while (!done)
        {
            if (Input.GetButtonDown("Jump"))
            {
                done = true;
            }
            yield return null;
        }
    }
}
