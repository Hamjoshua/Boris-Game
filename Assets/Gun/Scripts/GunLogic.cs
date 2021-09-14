using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunLogic : MonoBehaviour
{
    public Transform bulletPrefab;
    public bool isMelee;
    public int bulletSpeed;
    public int attackColldown;
    [SerializeField] private int attackTimer;
    void Start()
    {
        bulletSpeed = bulletPrefab.GetComponent<BulletLogic>().speed;
    }

    public void Shoot()
    {        
        if(attackTimer == 0)
        {            
            Transform bullet = Instantiate(bulletPrefab, transform.position, transform.rotation);
            if (isMelee)
            {
                bullet.GetComponent<Rigidbody>().isKinematic = true;
                Destroy(bullet.gameObject, 1);
            }
            else
            {
                bullet.GetComponent<Rigidbody>().velocity = transform.forward * bulletSpeed;
                Destroy(bullet.gameObject, 10);
            }
            attackTimer = attackColldown;
            StartCoroutine("ResetAttackTimer");
        }       
    }

    IEnumerator ResetAttackTimer()
    {
        for(; ; )
        {
            attackTimer--;
            if (attackTimer == 0)
            {
                StopAllCoroutines();
            }
            yield return new WaitForSeconds(.1f);
        }        
    }

    private void OnDisable()
    {
        attackTimer = 0;
    }
}
