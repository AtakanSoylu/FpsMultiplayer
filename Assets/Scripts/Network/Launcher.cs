using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;
using Photon.Realtime;

namespace MultiFps.Network 
{
    public class Launcher : MonoBehaviourPunCallbacks
    {
        public static Launcher Instance;

        [SerializeField] private TMP_InputField _roomNameinputField;
        [SerializeField] private TMP_Text _errorText;
        [SerializeField] private TMP_Text _roomNameText;
        [SerializeField] private Transform _roomListContent;
        [SerializeField] private Transform _playerListContent;
        [SerializeField] private GameObject _roomListItemPrefab;
        [SerializeField] private GameObject _playerListItemPrefab;
        [SerializeField] private GameObject _startgameButton;

        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            Debug.Log("Conneting  to master");

            PhotonNetwork.ConnectUsingSettings();
        }

        public override void OnConnectedToMaster()
        {
            Debug.Log("Connected to master");
            PhotonNetwork.JoinLobby();
            PhotonNetwork.AutomaticallySyncScene = true;
        }

        public override void OnJoinedLobby()
        {
            MenuManager.Instance.OpenMenu("title");
            Debug.Log("Joined Lobby");
            PhotonNetwork.NickName = "Player " + Random.Range(0, 1000);
        }

        public void CreateRoom()
        {
            if (!string.IsNullOrEmpty(_roomNameinputField.text))
            {
                PhotonNetwork.CreateRoom(_roomNameinputField.text);
                MenuManager.Instance.OpenMenu("loading");
            }
        }

        public override void OnJoinedRoom()
        {
            MenuManager.Instance.OpenMenu("room");
            _roomNameText.text = PhotonNetwork.CurrentRoom.Name;

            foreach (Transform child in _playerListContent)
            {
                Destroy(child.gameObject);
            }


            for (int i = 0; i < PhotonNetwork.PlayerList.Length; i++)
            {
                Instantiate(_playerListItemPrefab, _playerListContent).GetComponent<PlayerListItem>().SetUp(PhotonNetwork.PlayerList[i]);
            }

            _startgameButton.SetActive(PhotonNetwork.IsMasterClient);
        }

        public override void OnMasterClientSwitched(Player newMasterClient)
        {
            _startgameButton.SetActive(PhotonNetwork.IsMasterClient);
        }

        public override void OnCreateRoomFailed(short returnCode, string message)
        {
            _errorText.text = "Room Creation Failed: "+ message;
            MenuManager.Instance.OpenMenu("error");
        }

        public void StartGame()
        {
            PhotonNetwork.LoadLevel(1);
        }

        public void LeaveRoom()
        {
            PhotonNetwork.LeaveRoom();
            MenuManager.Instance.OpenMenu("loading");
        }

        public void JoinRoom(RoomInfo info)
        {
            PhotonNetwork.JoinRoom(info.Name);
            MenuManager.Instance.OpenMenu("loading");


        }

        public override void OnLeftRoom()
        {
            MenuManager.Instance.OpenMenu("title");
        }

        public override void OnRoomListUpdate(List<RoomInfo> roomList)
        {
            foreach (Transform item in _roomListContent)
            {
                Destroy(item.gameObject);
            }
            for (int i = 0; i < roomList.Count; i++)
            {
                if (roomList[i].RemovedFromList)
                    continue;
                Instantiate(_roomListItemPrefab, _roomListContent).GetComponent<RoomListItem>().SetUp(roomList[i]);
            }
        }

        public override void OnPlayerEnteredRoom(Player newPlayer)
        {
            Instantiate(_playerListItemPrefab, _playerListContent).GetComponent<PlayerListItem>().SetUp(newPlayer);
        }
    }
}
