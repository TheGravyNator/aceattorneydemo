using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpriteManager : MonoBehaviour
{
    public Character Prosecution;
    public Character Defense;
    public Character Witness;

    public Sprite[] Backgrounds;

    public Image CharacterImage;
    public Image BackgroundImage;

    public AudioSource sfx;

    private void Start()
    {
        StoryManager.OnDialogueChanged += DialogueChanged;
    }

    private void DialogueChanged(string name, string emote)
    {
        Character character = GetCharacter(name);
        sfx.clip = character.voice;

        if(CharacterImage != null)
            CharacterImage.sprite = GetCharacterSprite(character, emote);
        if (BackgroundImage != null)
            BackgroundImage.sprite = GetBackground(character);
    }

    private Character GetCharacter(string name)
    {
        if (name == "Franziska")
            return Prosecution;
        else if (name == "Phoenix")
            return Defense;
        else if (name == "Maya")
            return Witness;
        else throw new NoValidCharacterException();
        
    }

    private Sprite GetCharacterSprite(Character character, string emote)
    {
        if (character.name == "Franziska")
        {
            if (emote == "neutral")
                return Prosecution.sprites[(int)SpriteIndex.NEUTRAL];
            else if (emote == "shocked")
                return Prosecution.sprites[(int)SpriteIndex.SHOCKED];
            else if (emote == "thinking")
                return Prosecution.sprites[(int)SpriteIndex.THINKING];
            else if (emote == "pose")
                return Prosecution.sprites[(int)SpriteIndex.POSE];
            else return null;
        }
        else if (character.name == "Phoenix")
        {
            if (emote == "neutral")
                return Defense.sprites[(int)SpriteIndex.NEUTRAL];
            else if (emote == "shocked")
                return Defense.sprites[(int)SpriteIndex.SHOCKED];
            else if (emote == "thinking")
                return Defense.sprites[(int)SpriteIndex.THINKING];
            else if (emote == "pose")
                return Defense.sprites[(int)SpriteIndex.POSE];
            else return null;
        }
        else if (character.name == "Maya")
        {
            if (emote == "neutral")
                return Witness.sprites[(int)SpriteIndex.NEUTRAL];
            else if (emote == "shocked")
                return Witness.sprites[(int)SpriteIndex.SHOCKED];
            else if (emote == "thinking")
                return Witness.sprites[(int)SpriteIndex.THINKING];
            else if (emote == "pose")
                return Witness.sprites[(int)SpriteIndex.POSE];
            else return null;
        }
        else return null;
    }

    private Sprite GetBackground(Character character)
    {
        return Backgrounds[(int)character.type];
    }
}

public class NoValidCharacterException : Exception { }
