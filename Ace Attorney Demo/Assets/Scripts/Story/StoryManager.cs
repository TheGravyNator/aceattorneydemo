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

    public InventoryManager inventory;
    
    public delegate void DialogueChanged(string name, string emote);
    public static event DialogueChanged OnDialogueChanged;

    void Start()
    {
        DontDestroyOnLoad(this);
        story = new Story(inkFile.text);
    }
    
    void Update()
    {
        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)) && story.canContinue && textBox.canWrite && !inventory.isMenuActive)
        {
            var line = GetNextLine();
            textBox.WriteText(line.name, line.text);
            OnDialogueChanged(line.name, line.tag);
        }
    }

    private (string name, string text, string tag) GetNextLine()
    {
        string[] lineAndName = story.Continue().Split(':');
        string tag = story.currentTags[0];
        return (lineAndName[0], lineAndName[1].TrimStart(' '), tag);
    }
}