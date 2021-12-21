using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Gun : Item
{
    public abstract override void Use();

    public GameObject bulletImpact;
    public GameObject shootParticle;
    public Transform gunShootPoint;

}
