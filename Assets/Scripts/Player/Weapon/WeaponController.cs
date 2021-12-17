using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MultiFps.Player
{
    public class WeaponController : MonoBehaviour
    {
        [SerializeField] private Transform _mainCam; 
        [SerializeField] private GameObject _bullet;
        private void Update()
        {
            if (Input.GetButtonDown("Fire1"))
            {
                Shoot();
            }
        }

        public void Shoot()
        {
            RaycastHit hit;

            if(Physics.Raycast(_mainCam.position, _mainCam.forward, out hit))
            {
                Debug.Log(hit.transform.gameObject.name);
            }
        }
    }
}