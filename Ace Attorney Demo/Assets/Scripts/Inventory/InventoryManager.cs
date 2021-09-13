using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public Queue<Profile> Profiles { get; set; }
    public Queue<Evidence> Evidence { get; set; }

    public Sprite image;
    
    void Start()
    {
        Profiles = new Queue<Profile>();

        Profiles.Enqueue(new Profile
        {
            Name = "Aqua",
            Description = "A useless goddess.",
            Age = "???",
            Gender = "Female",
            Image = image
        });
    }
}
