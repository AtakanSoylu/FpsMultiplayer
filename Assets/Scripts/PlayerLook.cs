using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    [Header("References")] 
    [SerializeField] private Transform cameraHolder;
    [SerializeField] private Transform orientation;
    [SerializeField] private Transform characterModel;
    [SerializeField] private Transform characterSpine;

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
        characterSpine.rotation = Quaternion.Euler(xRotation,0,0);
        characterModel.rotation = Quaternion.Euler(0,yRotation,0);
        orientation.rotation = Quaternion.Euler(0f, yRotation, 0f);
        cameraHolder.localRotation = Quaternion.Euler(xRotation, yRotation, 0f);

    }
}
