using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    [Header("References")] 
    [SerializeField] private Transform cameraHolder;
    [SerializeField] private Transform orientation;

    [Header("Look Settings")]
    [SerializeField] private float sensX;
    [SerializeField] private float sensY;

    private float yRotation;
    private float xRotation;

    [SerializeField] private PhotonView PV;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        PV = GetComponent<PhotonView>();
    }

    private void Update()
    {
        if (!PV.IsMine)
        {
            return;
        }
        float mouseX = Input.GetAxisRaw("Mouse X") * 0.1f;
        float mouseY = Input.GetAxisRaw("Mouse Y") * 0.1f;

        yRotation += mouseX * sensX;
        xRotation -= mouseY * sensY;

        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        orientation.rotation = Quaternion.Euler(0f, yRotation, 0f);
        cameraHolder.localRotation = Quaternion.Euler(xRotation, yRotation, 0f);

    }
}
