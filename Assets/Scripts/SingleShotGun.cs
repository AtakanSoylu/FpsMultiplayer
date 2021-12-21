using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleShotGun : Gun
{
    [SerializeField] private Camera _camera;

    public override void Use()
    {
        Shoot();
    }

    void Shoot()
    {
        Ray ray = _camera.ViewportPointToRay(new Vector3(0.5f, 0.5f));
        ray.origin = _camera.transform.position;
        if (Physics.Raycast(ray,out RaycastHit hit))
        {
            hit.collider.GetComponent<IDamageable>().TakeDamage(((GunInfo)ItemInfo).damage);
        }
    }
}
