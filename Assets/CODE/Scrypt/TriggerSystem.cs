using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum TRIGGER_TYPE
{
    TELEPORT,
    LIGHTBUG,
    ITEM_APPEARS,
    ITEAM_DISAPEARS,
    PLAY_SONG,
    START_EVENT
}
public class TriggerSystem : MonoBehaviour
{
    public static TriggerSystem instance;
    public bool despawnTrigger; // if yes, the trigger despawn after is activation
    public TRIGGER_TYPE triggerType;
    [Header("lightbug option")]
    public GameObject[] lightsgroup;
    public float lightBugDuration;
    bool _enCoursDexecution = false;
    float _lightBugTime = 0;
    [Header("TeleportTriggerOption")]
    public Vector3 teleportPosition;
    public GameObject objectToTeleport;
    public bool blockX,blockY,blockZ;
    [Header("ItemAppearsOrDesapeartTriggerOption")]
    public GameObject objectToAppears;
    public Vector3 appearsPosition;
    public bool appearPositionEnable;
    [Header("playsong trigger")]
    public AudioSource sourceToEnable;
    public bool enableIt;
    [Header("event trigger")]
    public GameObject event_trigger;

    private void Awake()
    {
        instance = this;
    }
   private void Update() 
    {
        if(_enCoursDexecution)
            _lightBugTime+= Time.deltaTime;

    }
    public void DespawnThis()
    {
        if (despawnTrigger)
            this.transform.position = new Vector3(-999,-999,-999);    }
    public void TeleportTrigger(Vector3 newPosition, GameObject obj)
    {
        Vector3 finalposition = newPosition;
        if(blockX)
            finalposition.x = obj.transform.position.x;
        if(blockY)
            finalposition.y = obj.transform.position.y;
        if(blockZ)
            finalposition.z = obj.transform.position.z;
        if(obj.GetComponent<CharacterController>() != null)
        {
            obj.GetComponent<CharacterController>().enabled = false;
            obj.transform.position = finalposition;
            obj.GetComponent<CharacterController>().enabled = true;
        }
        else
        {
            obj.transform.position = finalposition;
        }
        DespawnThis();
    }

    public void ItemAppears()
    {
        objectToAppears.gameObject.SetActive(true);
        if(appearPositionEnable)
            objectToAppears.transform.position = appearsPosition;

        DespawnThis();
    }
    public void ItemDesappears()
    {
        objectToAppears.gameObject.SetActive(false);
        DespawnThis();
    }
    public void PlaySong()
    {
        

        if (enableIt)
        sourceToEnable.Play();
        else
        sourceToEnable.Stop();
        DespawnThis();

    }
    public void StartEvenementTrigger()
    {
        //ballon event
        //event_trigger.GetComponent<quickpulse>().StartEvent();
        DespawnThis();
    }
    public void LightBug()
    {
        DespawnThis();
        StartCoroutine(LightBugCouroutine());
    }
    IEnumerator LightBugCouroutine()
    {
        Debug.Log("in couroutine");
        float r = Random.Range(.01f,.3f);        
        yield return new WaitForSeconds(r);        
        foreach (var item in lightsgroup)
        {
            item.gameObject.SetActive(!item.gameObject.active);
        }
        if(_lightBugTime < lightBugDuration)
        {
            _enCoursDexecution = true;
            StartCoroutine(LightBugCouroutine());
        }
        else
        {
            _enCoursDexecution = false;
            foreach (var item in lightsgroup)
            {
                item.gameObject.SetActive(true);
            }
            DespawnThis();
            _lightBugTime = 0;
        }
    }
}
