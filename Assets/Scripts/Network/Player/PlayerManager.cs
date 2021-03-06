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
        private GameObject contoller;
        
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
            Transform spawnPoint = SpawnManager.Instance.GetSpawnPoint();
            contoller = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PlayerController"), spawnPoint.position, spawnPoint.rotation,0,new object[]{PV.ViewID});
            
        }
        
        public IEnumerator Die()
        {
            PhotonNetwork.Destroy(contoller);
            yield return new WaitForSeconds(1);
            CreateController();
        }
    }
}