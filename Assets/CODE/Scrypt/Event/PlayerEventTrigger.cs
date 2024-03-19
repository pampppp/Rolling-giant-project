using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEventTrigger : MonoBehaviour
{
    
    private void OnTriggerEnter(Collider other) 
    {
        if(other.tag == "Event")
        {
            other.GetComponent<EventSystem>().StartEvenement();
            other.GetComponent<EventSystem>().DesableTrigger();
        }
    }
}
