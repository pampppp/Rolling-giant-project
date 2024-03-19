using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventSpawnObject : EventSystem
{
    [Tooltip("select object to spawn or unspawn")]    
    public GameObject obj;
    [Tooltip("define if the object spawn or desapear")]
    public bool spawnObject;    
    [Tooltip("define if the object desappear after n time")]
    public bool isDesappear;
    public float countdownTime;
    [Tooltip("define if the location of the object is diferent like the inspector")]
    public bool isSpawnAtOtherLocation;
    public Vector3 newLocation;
    public Quaternion newRotation;

    public override void StartEvenement()
    {
        if (spawnObject)
        {
            if(isSpawnAtOtherLocation)
            {
                obj.transform.position = newLocation;
            }
            obj.SetActive(true);   
            if(isDesappear)
            {
                StartCoroutine(UnspawnObjectCouroutine());
            }
        }
        else
        {
            obj.SetActive(false);
        }
    }
    IEnumerator UnspawnObjectCouroutine()
    {
        yield return new WaitForSeconds(countdownTime);
        obj.SetActive(false);
    }
}
