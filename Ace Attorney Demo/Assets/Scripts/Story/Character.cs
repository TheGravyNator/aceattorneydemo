using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
[CreateAssetMenu(fileName = "New Character", menuName = "Characters/New Character")]
public class Character : ScriptableObject
{
    public string firstName;
    public string lastName;
    public CharacterType type;
    public Sprite[] sprites;
    public AudioClip voice;
}
