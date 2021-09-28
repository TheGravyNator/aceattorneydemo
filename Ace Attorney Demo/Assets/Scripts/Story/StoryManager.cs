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
    }
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A) && story.canContinue && textBox.canWrite)
        {
            var line = GetNextLine();
            textBox.WriteText(line.name, line.text);
        }
    }

    private (string name, string text) GetNextLine()
    {
        string[] lineAndName = story.Continue().Split(':');
        return (lineAndName[0], lineAndName[1].TrimStart(' '));
    }
}