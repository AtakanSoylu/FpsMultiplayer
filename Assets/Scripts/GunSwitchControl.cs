using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunSwitchControl : MonoBehaviour
{
    public void SwitchGunTrigger()
    {
        PlayerMovement.Instance.switchGun = true;
    }

}
