using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct Character
{
    public string name;
    public CharacterType type;
    public Sprite[] sprites;
    public AudioClip voice;
}
