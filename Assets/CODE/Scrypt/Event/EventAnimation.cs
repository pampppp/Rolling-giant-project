using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventAnimation : EventSystem
{
    public Animator animatorObj;
    public override void StartEvenement()
    {
        Debug.Log("in animation");
        animatorObj.enabled = true;
    }
}
