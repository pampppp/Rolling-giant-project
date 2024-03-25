using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventSpawnObject : EventSystem
{
#region Variables
    [Tooltip("select object to spawn or unspawn")]    
    [SerializeField] private GameObject[] _obj;
    [Tooltip("define if the object spawn or desapear")]
    [SerializeField] private bool _isSpawnObject;    
    [Tooltip("define if the object desappear after n time")]
    [SerializeField] private bool _isWillDesappear;
    [SerializeField] private float _timeBeforDesappear;
    [Tooltip("define if the location of the object is diferent like the inspector")]
    [SerializeField] private bool _isSpawnAtOtherLocation;
    [SerializeField] private Vector3 _newLocation;
#endregion
    
#region Fonction
    public override void StartEvenement() 
    {
        //check if we spawn or unspawn the gameobject
        if (_isSpawnObject)
        {
            //check if need to change the gameobject's position
            if(_isSpawnAtOtherLocation)
            {
                _obj[0].transform.position = _newLocation;
            }
            //spawn gameobject
            foreach (GameObject o in _obj)
            {
                o.SetActive(true); 
            }
              
            //check if gameobject desapear after he is spawning
            if(_isWillDesappear)
            {
                StartCoroutine(UnisSpawnObjectCouroutine());
            }
        }
        else
        {
            //desable the object
            foreach (GameObject o in _obj)
            {
                    o.SetActive(false); 
            }        
        }
    }
#endregion
#region couroutine
    IEnumerator UnisSpawnObjectCouroutine()
    {
        //couroutine for desable object after is spawning after n time
        yield return new WaitForSeconds(_timeBeforDesappear);
        foreach (GameObject o in _obj)
        {
            o.SetActive(true); 
        } 
    }
#endregion
    
    
}
