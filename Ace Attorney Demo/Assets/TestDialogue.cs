using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;

public class TestDialogue : MonoBehaviour
{
    public TextAsset inkJSON;
    private Story story;

    void Start()
    {
        story = new Story(inkJSON.text);
        Debug.Log(story.Continue());
    }

    void Update()
    {
        
    }
}
