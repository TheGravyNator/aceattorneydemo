using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Location
{
    public string Name { get; set; }
    public Image Image { get; set; }
    public bool isVisited { get; set; }
    public Location[] ConnectedLocations { get; set; }

    public Location()
    {
        ConnectedLocations = new Location[4];
    }
}
