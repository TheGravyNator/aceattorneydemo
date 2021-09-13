using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavigationExplorer : MonoBehaviour
{
    public Location CurrentLocation { get; set; }

    public void GoToLocation(Location location)
    {
        Location newLocation = null; 

        foreach (Location loc in CurrentLocation.ConnectedLocations)
        {
            if (location == loc)
            {
                newLocation = location;
                break;
            }
        }

        if (newLocation == null)
            throw new Exception("Can't access that map!");
        else
            CurrentLocation = newLocation;
    }
}
