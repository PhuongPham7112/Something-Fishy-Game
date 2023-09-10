using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ValvePipeItem : InteractableFunctionality
{
    [SerializeField] WaterPlane waterPlane;

    // Increase the water level again
    public override void Action(bool isClicked, string key = "")
    {
        waterPlane.RestoreWaterLevel();
    }
}