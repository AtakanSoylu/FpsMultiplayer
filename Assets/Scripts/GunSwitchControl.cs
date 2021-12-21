using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunSwitchControl : MonoBehaviour
{
    public void SwitchGunTrigger()
    {
        PlayerController.Instance.switchGun = true;
    }

}
