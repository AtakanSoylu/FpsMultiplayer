using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Unity.Mathematics;
using UnityEngine;

public class SingleShotGun : Gun
{
    private PhotonView pv;
    [SerializeField] private Camera _camera;
    
    

    private void Awake()
    {
        pv = GetComponent<PhotonView>();
    }

    public override void Use()
    {
        Shoot();
    }

    void Shoot()
    {
        print("Shoot");
        Instantiate(shootParticle,gunShootPoint.position, gunShootPoint.rotation,transform);
        Destroy(shootParticle,2);
        Ray ray = _camera.ViewportPointToRay(new Vector3(0.5f, 0.5f));
        ray.origin = _camera.transform.position;
        if (Physics.Raycast(ray,out RaycastHit hit))
        {
            hit.collider.GetComponent<IDamageable>()?.TakeDamage(((GunInfo)ItemInfo).damage);
            pv.RPC("RPC_Shoot",RpcTarget.All,hit.point,hit.normal,hit.transform);
        }
    }

    [PunRPC]
    void RPC_Shoot(Vector3 hitPosition, Vector3 hitNormal,Transform hitTransform)
    {
        Collider[] colliders = Physics.OverlapSphere(hitPosition, 0.3f);
        if (colliders.Length != 0)
        {
            GameObject bulletImpactObj = Instantiate(bulletImpact, hitPosition + hitNormal * 0.001f, Quaternion.LookRotation(hitNormal,Vector3.up) * bulletImpact.transform.rotation);
            Destroy(bulletImpactObj,5f);
            bulletImpactObj.transform.SetParent(colliders[0].transform);    
        }
        
    }
}
