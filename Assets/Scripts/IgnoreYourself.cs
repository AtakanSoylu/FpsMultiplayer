using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class IgnoreYourself : MonoBehaviour
{
    public Camera _camera;
    private PhotonView _photonView;
    public LayerMask himself;

    private void Awake()
    {
        _photonView = GetComponent<PhotonView>();
    }

    private void Start()
    {
    }
}
