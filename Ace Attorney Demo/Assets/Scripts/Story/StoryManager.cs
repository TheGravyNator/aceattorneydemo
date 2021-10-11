using Ink.Runtime;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StoryManager : MonoBehaviour
{
    public Story story;
    public TextAsset inkFile;

    public TextboxController textBox;

    public GameObject CrossExaminationUI;
    public GameObject InGameUI;
    public TextboxController CrossExaminationTextBox;

    public Image FadeScreen;

    public MusicManager music;

    public AudioSource sfx;
    public AudioSource exclamationSfx;

    public AudioClip ObjectionSfx;
    public AudioClip HoldItSfx;

    public GameObject ObjectionUI;
    public GameObject HoldItUI;

    public InventoryManager inventory;
    
    public delegate void DialogueChanged(string name, string emote);
    public static event DialogueChanged OnDialogueChanged;

    private bool title = true;
    private string evidence;

    List<Choice> choices;

    void Start()
    {
        story = new Story(inkFile.text);

        CrossExaminationStart();
    }
    
    void Update()
    {
        if (!CrossExaminationTextBox.writeTitle)
        {
            StartTestimony();
        }
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

    private void StoryEnd()
    {
        SceneManager.LoadScene("Main Menu");
    }

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
        CrossExaminationUI.GetComponentInChildren<Animation>().Play();
        string title = story.Continue();
        evidence = story.currentTags[0];
        CrossExaminationTextBox.WriteTitle(title);
    }

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
        CrossExaminationUI.SetActive(false);
        InGameUI.SetActive(true);
        var line = GetNextLine();
        textBox.WriteText(line.name, line.text);
        OnDialogueChanged(line.name, line.tag);
    }

    public void MakeChoice(int index)
    {
        Choice choiceSelected = choices[index];
        story.ChooseChoiceIndex(choiceSelected.index);
    }

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

    public void PressStatement()
    {
        if (choices != null && choices.Count >= 3)
        {
            StartCoroutine(HoldIt());
        }
    }

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