using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private Transform playerOrientation;

    [SerializeField] private float cameraSensitivityX = 2.0f;
    [SerializeField] private float cameraSensitivityY = 2.0f;
    [SerializeField] private float rotationClamp = 45.0f;

    private float xRotation;
    private float yRotation;


    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        float mouseInputX = Input.GetAxis("Mouse X") * Time.deltaTime * cameraSensitivityX;
        float mouseInputY = Input.GetAxis("Mouse Y") * Time.deltaTime * cameraSensitivityY;

        yRotation += mouseInputX;
        xRotation -= mouseInputY;

        xRotation = Mathf.Clamp(xRotation, -rotationClamp, rotationClamp);

        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        playerOrientation.rotation = Quaternion.Euler(0, yRotation, 0);

    }
}
