using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public enum EVENEMENT_TYPE
{
    SPAWN_OBJECT
}

public abstract class EventSystem : MonoBehaviour
{
    [Tooltip("define if the trigger desapear after activation")]
    public bool unspawnTrigger;
    public abstract void StartEvenement(); // this function is called when the player enter in trigger, it will start an event
    public void DesableTrigger()
    {
        if (unspawnTrigger)
        {
            this.gameObject.SetActive(false);
        }
    }
}
