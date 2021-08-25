using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletLogic : MonoBehaviour
{
    public int damage = 50;
    public int speed = 100;
    void Start()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        Creature creature = collision.gameObject.GetComponent<Creature>();
        if(creature)
        {
            creature.TakeDamage(damage);
            Destroy(gameObject);
        }
       
    }
}
