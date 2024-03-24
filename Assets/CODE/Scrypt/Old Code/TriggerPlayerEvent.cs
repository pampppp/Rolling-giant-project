using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerPlayerEvent : MonoBehaviour
{
   
    private void OnTriggerEnter(Collider other) 
    {  
        if( other.GetComponent<TriggerSystem>() != null)
        {
            if(other.GetComponent<TriggerSystem>().triggerType == TRIGGER_TYPE.TELEPORT)
            {
                other.GetComponent<TriggerSystem>().TeleportTrigger(other.GetComponent<TriggerSystem>().teleportPosition, other.GetComponent<TriggerSystem>().objectToTeleport);
            }
            else if(other.GetComponent<TriggerSystem>().triggerType == TRIGGER_TYPE.ITEM_APPEARS)
            {
                other.GetComponent<TriggerSystem>().ItemAppears();
            }
            else if(other.GetComponent<TriggerSystem>().triggerType == TRIGGER_TYPE.ITEAM_DISAPEARS)
            {
                other.GetComponent<TriggerSystem>().ItemDesappears();
            }
            else if(other.GetComponent<TriggerSystem>().triggerType == TRIGGER_TYPE.ITEAM_DISAPEARS)
            {
                other.GetComponent<TriggerSystem>().ItemDesappears();
            }
            else if(other.GetComponent<TriggerSystem>().triggerType == TRIGGER_TYPE.LIGHTBUG)
            {
                other.GetComponent<TriggerSystem>().LightBug();
            }
            else if(other.GetComponent<TriggerSystem>().triggerType == TRIGGER_TYPE.PLAY_SONG)
            {
                other.GetComponent<TriggerSystem>().PlaySong();
            }      
            else if(other.GetComponent<TriggerSystem>().triggerType == TRIGGER_TYPE.START_EVENT)
            {
                other.GetComponent<TriggerSystem>().StartEvenementTrigger();
            }    
        }
    }
}
