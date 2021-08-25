using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunLogic : MonoBehaviour
{
    public Transform bulletPrefab;
    public bool isMelee;   
    public int bulletSpeed;
    void Start()
    {
        bulletSpeed = bulletPrefab.GetComponent<BulletLogic>().speed;
    }

    public void Shoot()
    {
        Transform parentTransform = transform.parent;
        Vector3 bulletPos = transform.position + (transform.forward * 2) - (transform.up * 2);

        Transform bullet = Instantiate(bulletPrefab, bulletPos, Quaternion.identity);        

        if (isMelee)
        {
            bullet.SetParent(transform);
            bullet.GetComponent<Rigidbody>().isKinematic = true;
            Destroy(bullet.gameObject, 2);            
        }
        else
        {
            bullet.GetComponent<Rigidbody>().velocity = transform.forward * bulletSpeed;
            
            Destroy(bullet.gameObject, 10);
        }

    }
}
