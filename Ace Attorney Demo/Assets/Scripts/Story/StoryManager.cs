using Ink.Runtime;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StoryManager : MonoBehaviour
{
    // Move to InkParsingHandler
    public Story story;
    public TextAsset inkFile;
     
    public TextboxController textBox;
    public TextboxController CrossExaminationTextBox;
    
    // Move to StoryUIHandler
    public GameObject CrossExaminationUI;
    public GameObject InGameUI;
    public Image FadeScreen;
    
    // Subscribe MusicManager to event
    public MusicManager music;

    // Move to StorySFXHandler
    public AudioSource sfx;
    public AudioSource exclamationSfx;

    public AudioClip ObjectionSfx;
    public AudioClip HoldItSfx;
    
    // Move to StoryUIHandler
    public GameObject ObjectionUI;
    public GameObject HoldItUI;
    
    // Related to the bools in the Update function, rework that
    public InventoryManager inventory;
    
    // Change some things regarding this event, refer to AdvanceDialogue comment
    public delegate void DialogueChanged(string name, string emote);
    public static event DialogueChanged OnDialogueChanged;

    // Figure out what to do with this title bool
    private bool title = true;
    
    private string evidence;

    List<Choice> choices;

    void Start()
    {
        // Move to InkParserHandler
        story = new Story(inkFile.text);

        CrossExaminationStart();
    }
    
    void Update()
    {
        // Find a better solution to work this out
        if (!CrossExaminationTextBox.writeTitle)
        {
            StartTestimony();
        }
        
        // What's going on with all these bools? Can I handle this better? Also maybe move input to InputManager
        if (Input.GetKeyDown(KeyCode.Space) && textBox.canWrite && !inventory.isMenuActive && !title)
        {
            if (choices != null && choices.Count >= 3)
                MakeChoice((int)ChoiceSelection.Continue);
            if (story.canContinue)
                AdvanceDialogue();
            else
                StartCoroutine(FadeToBlack());
        }
    }

    // You're fine, bud
    private void StoryEnd()
    {
        SceneManager.LoadScene("Main Menu");
    }
    
    // Move to StoryUIHandler
    IEnumerator FadeToBlack()
    {
        for (float alpha = 0f; FadeScreen.color.a < 1f; alpha += 0.1f)
        {
            FadeScreen.color = new Color(FadeScreen.color.r, FadeScreen.color.g, FadeScreen.color.b, alpha);
            yield return new WaitForSeconds(0.1f);
        }
        StoryEnd();
    }

    private void CrossExaminationStart()
    {
        // Move to StoryUIHandler
        CrossExaminationUI.GetComponentInChildren<Animation>().Play();
        CrossExaminationTextBox.WriteTitle(title);
        
        // Move to InkParsingHandler
        string title = story.Continue();
        evidence = story.currentTags[0];
    }

    // Figure out how to do this better
    private void StartTestimony()
    {
        if (title)
        {
            title = false;
            music.PlayTrial();
        }
    }

    public void AdvanceDialogue()
    {
        // Move to StoryUIHandler
        CrossExaminationUI.SetActive(false);
        InGameUI.SetActive(true);
        
        var line = GetNextLine();
        
        // Rewrite the event on this, move textbox call to StoryUIHandler
        textBox.WriteText(line.name, line.text);
        OnDialogueChanged(line.name, line.tag);
    }
    
    // Move to InkParserHandler
    public void MakeChoice(int index)
    {
        Choice choiceSelected = choices[index];
        story.ChooseChoiceIndex(choiceSelected.index);
    }

    // Move to InkParserHandler
    private (string name, string text, string tag) GetNextLine()
    {
        textBox.textField.color = Color.white;
        string[] lineAndName = story.Continue().Split(':');
        string[] tags = story.currentTags[0].Split(' ');
        choices = story.currentChoices;
        if (tags.Length > 1)
        {
            if (tags[1] == "CrossExamine")
                textBox.textField.color = Color.green;
            else if (tags[1] == "clue")
                sfx.Play();
        }
        string tag = tags[0];
        return (lineAndName[0], lineAndName[1].TrimStart(' '), tag);
    }
    
    // Create event for this
    public void PressStatement()
    {
        if (choices != null && choices.Count >= 3)
        {
            StartCoroutine(HoldIt());
        }
    }

    // Create event for this
    public void PresentEvidence(Clue item)
    {
        if (choices.Count == 4 && item.name == evidence)
        {
            music.Stop();
            StartCoroutine(Objection(true));
        }
        else
        {
            StartCoroutine(Objection(false));
        }
    }
    
    // Move to StoryUIManager and StorySFXManager
    IEnumerator Objection(bool right)
    {
        exclamationSfx.PlayOneShot(ObjectionSfx);
        ObjectionUI.SetActive(true);
        ObjectionUI.GetComponent<Animation>().Play();
        while (ObjectionUI.GetComponent<Animation>().isPlaying)
        {
            yield return new WaitForSeconds(1f);
        }
        if (right)
        {
            music.PlayCornered();
            MakeChoice((int)ChoiceSelection.PresentRight);
        }
        else
            MakeChoice((int)ChoiceSelection.PresentWrong);
        ObjectionUI.SetActive(false);
        AdvanceDialogue();
    }

    // Move to StoryUIManager and StorySFXManager
    IEnumerator HoldIt()
    {
        exclamationSfx.PlayOneShot(HoldItSfx);
        HoldItUI.SetActive(true);
        HoldItUI.GetComponent<Animation>().Play();
        while (HoldItUI.GetComponent<Animation>().isPlaying)
        {
            yield return new WaitForSeconds(1f);
        }
        HoldItUI.SetActive(false);

        MakeChoice((int)ChoiceSelection.Press);
        AdvanceDialogue();
    }
}

public enum ChoiceSelection
{
    Press,
    PresentWrong,
    Continue,
    PresentRight
}
