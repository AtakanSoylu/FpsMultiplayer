using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UsernameDisplay : MonoBehaviour
{
    [SerializeField] private PhotonView playerPV;
    [SerializeField] private TMP_Text _nameText;

    private void Start()
    {
        _nameText.text = playerPV.Owner.NickName;
    }
}
