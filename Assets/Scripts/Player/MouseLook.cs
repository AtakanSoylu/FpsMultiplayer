using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MultiFps.Player
{
    public class MouseLook : MonoBehaviour
    {
        [Range(50,500)]
        [SerializeField] private float _sens;
        [SerializeField] private Transform _characterTransform;

        private float _xRot;

        private void Update()
        {
            float rotX = Input.GetAxis("Mouse X") * _sens * Time.deltaTime;
            float rotY = Input.GetAxis("Mouse Y") * _sens * Time.deltaTime;

            _xRot -= rotY;
            _xRot = Mathf.Clamp(_xRot, -80, 80);

            transform.localRotation = Quaternion.Euler(_xRot, 0f, 0f);

            _characterTransform.Rotate(Vector3.up * rotX);
        }
    }
}