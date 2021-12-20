using Photon.Pun;
using UnityEngine;
using TMPro;
using Photon.Realtime;
namespace MultiFps.Network
{
    public class PlayerListItem : MonoBehaviourPunCallbacks
    {
        [SerializeField] private TMP_Text _text;
        [SerializeField] private Player _player;

        public void SetUp(Player player)
        {
            _player = player;
            _text.text = player.NickName;
        }

        public override void OnPlayerLeftRoom(Player otherPlayer)
        {
            if(_player == otherPlayer)
            {
                Destroy(gameObject);
            }
        }

        public override void OnLeftRoom()
        {
            Destroy(gameObject);
        }
    }
}