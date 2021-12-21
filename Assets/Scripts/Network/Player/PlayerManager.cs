using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.IO;

namespace MultiFps.Network
{
    public class PlayerManager : MonoBehaviour
    {

        PhotonView PV;

        private void Awake()
        {
            PV = GetComponent<PhotonView>();
        }

        private void Start()
        {
            if (PV.IsMine)
            {
                CreateController();
            }
        }
        public void CreateController()
        {
            Debug.Log("Instantiated Player Controller");
            PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PlayerController"),Vector3.zero,Quaternion.identity);
        }
    }
}