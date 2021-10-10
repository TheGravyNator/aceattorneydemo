using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
[CreateAssetMenu(fileName = "New Profile", menuName = "Clues/New Profile")]
public class Profile : Clue
{
    [Header("Profile Data")]
    public string Age;
}
