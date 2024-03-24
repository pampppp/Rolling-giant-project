using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraPosition : MonoBehaviour
{
    [SerializeField] private Transform cameraPosition;
    [SerializeField] private float cameraPositionX, camerPositionY, cameraPositionZ;

    private void Update()
    {
        transform.position = cameraPosition.transform.position + new Vector3 (cameraPositionX, camerPositionY, cameraPositionZ);
    }
}
