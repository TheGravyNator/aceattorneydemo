using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpriteManager : MonoBehaviour
{
    public List<Character> Characters;

    public Sprite[] Backgrounds;

    public Image CharacterImage;
    public Image BackgroundImage;

    public AudioSource sfx;

    public List<EmoteStruct> Emotes;

    private void Start()
    {
        StoryManager.OnDialogueChanged += DialogueChanged;
    }

    private void DialogueChanged(string name, string emote)
    {
        Character character = GetCharacter(name);
        if(sfx != null)
            sfx.clip = character.voice;

        if(CharacterImage != null)
            CharacterImage.sprite = GetCharacterSprite(character, emote);
        if (BackgroundImage != null)
            BackgroundImage.sprite = GetBackground(character);
    }

    private Character GetCharacter(string name)
    {
        Character character = Characters.Find(c => c.firstName == name);
        return character != null ? character : throw new NoValidCharacterException();
    }

    private Sprite GetCharacterSprite(Character character, string emote)
    {
        SpriteIndex index = Emotes.Find(e => e.emoteName == emote).emoteIndex;
        return character.sprites[(int)index];
    }

    private Sprite GetBackground(Character character)
    {
        return Backgrounds[(int)character.type];
    }
}

public class NoValidCharacterException : Exception { }

[Serializable]
public struct EmoteStruct
{
    public string emoteName;
    public SpriteIndex emoteIndex;
}
