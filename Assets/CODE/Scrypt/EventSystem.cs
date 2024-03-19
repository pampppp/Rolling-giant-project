using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public enum EVENEMENT_TYPE
{
    SPAWN_OBJECT
}

public abstract class EventSystem : MonoBehaviour
{
    [SerializeField]private EVENEMENT_TYPE _eventType;
    [Tooltip("define if the trigger desapear after activation")]
    public bool unspawnTrigger;
    public abstract void StartEvenement();
}
