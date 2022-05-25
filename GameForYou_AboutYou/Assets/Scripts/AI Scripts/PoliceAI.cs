using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoliceAI : CarAIHandler
{
    public PackagePicker PackagePicker;
   

    private void Start()
    {
        MaxSpeed = 0;
        skillLevel = 0.6f;
    }
    private void Update()
    {
        FollowPlayerWhenPackageCountEquals();
    }

    void FollowPlayerWhenPackageCountEquals()
    {
        if(PackagePicker.packageCount == 4)
        {
            MaxSpeed = 16;
            skillLevel = 0.8f;
        }
    }
}
