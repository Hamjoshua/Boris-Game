using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieScript : MonoBehaviour
{
    public float speed = 10f;
    public int distance;
    public GunLogic gun;    
    public Animator animator;    
    Creature creature;

    public Transform player;

    private void Start()
    {
        creature = GetComponent<Creature>();
    }
    void Update()
    {
        if(!creature.died)
        {
            transform.LookAt(player);
            if (!(Vector3.Distance(transform.position, player.transform.position) < 1.0))
            {
                creature.MoveOn(Vector3.forward);
                if(animator.isActiveAndEnabled)
                {
                    animator.SetInteger("Hp", creature.health);
                }
            }
            Shoot();
        }        
    }

    void Shoot()
    {        
        if(Vector3.Distance(transform.position, player.position) <= distance)
        {
            gun.Shoot();         
        }        
    }
}
