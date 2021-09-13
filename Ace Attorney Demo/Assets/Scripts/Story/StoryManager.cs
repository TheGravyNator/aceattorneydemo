using Ink.Runtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoryManager : MonoBehaviour
{
    public Story story;
    public TextAsset inkFile;

    public TextboxController textBox;

    void Start()
    {
        DontDestroyOnLoad(this);
        story = new Story(inkFile.text);
        Debug.Log(story.Continue());
    }
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A) && story.canContinue && textBox.canWrite)
        {
            textBox.WriteText(story.Continue());
        }
    }
}
