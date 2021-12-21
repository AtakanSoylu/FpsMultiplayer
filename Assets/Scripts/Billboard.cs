using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Billboard : MonoBehaviour
{
    [SerializeField] private Camera cam;

    private void Update()
    {
        if (cam == null) return;
        transform.LookAt(cam.transform);
        transform.Rotate(Vector3.up * 100);
    }
}
