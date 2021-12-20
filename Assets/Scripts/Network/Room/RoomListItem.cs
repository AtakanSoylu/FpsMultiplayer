using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Realtime;

namespace MultiFps.Network
{
    public class RoomListItem : MonoBehaviour
    {
        [SerializeField] private TMP_Text _roomName;

        public RoomInfo _info;

        public void SetUp(RoomInfo info)
        {
            _info = info;
            _roomName.text = info.Name;
        }

        public void OnClick()
        {
            Launcher.Instance.JoinRoom(_info);
        }
    }
}