using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class Clue : ScriptableObject
{
    [Header("General Data")]
    public string Name;
    public string Description;
    public Sprite Image;
}
