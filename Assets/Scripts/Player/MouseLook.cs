using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MultiFps.Player
{
    public class MouseLook : MonoBehaviour
    {
        public Transform player;

        void Update() {
            transform.position = player.transform.position;
        }
    }
}