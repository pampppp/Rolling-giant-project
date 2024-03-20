using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventTeleport : EventSystem
{
    [SerializeField] private Transform objToTeleport;
    [SerializeField] private bool _isXMoving;
    [SerializeField] private bool _isYMoving;
    [SerializeField] private bool _isZMoving;
    [SerializeField] private Vector3 _newPosition;
    public override void StartEvenement()
    {
        float x = objToTeleport.position.x;
        float y = objToTeleport.position.y;
        float z = objToTeleport.position.z;
        if(objToTeleport.GetComponent<CharacterController>() != null)
        {
            objToTeleport.GetComponent<CharacterController>().enabled = false;
        }
        if(_isXMoving)
        {
            x = _newPosition.x;
        }
        if(_isYMoving)
        {
            y = _newPosition.y;
        }
        if(_isZMoving)
        {
            z = _newPosition.z;
        }

        objToTeleport.position = new Vector3(x,y,z);

        if(objToTeleport.GetComponent<CharacterController>() != null)
        {
            objToTeleport.GetComponent<CharacterController>().enabled = true;
        }  
        
    }
}
