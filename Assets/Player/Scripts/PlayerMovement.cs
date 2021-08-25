using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Start is called before the first frame update
    Creature creature;

    public int attackCooldown = 100;
    private int attackTimer;
    private CharacterController characterController;    
    public GunLogic gun;
    public Animator animator;

    Camera _cam;
    Vector3 targetDir;
    void Start()
    {        
        creature = GetComponent<Creature>();        
    }

    // Update is called once per frame
    private void Update()
    {
        if (!creature.died)
        {
            MakeShoot();
        }
    }

    private void FixedUpdate()
    {
        if(!creature.died)
        {
            float x = Input.GetAxis("Horizontal");            
            float z = Input.GetAxis("Vertical");
            Animate(x, z);
            Vector3 movement = new Vector3(x, 0, z);            

            creature.MoveOn(movement);          
            
        }        
    }
    
    private void MakeShoot()
    {
        --attackTimer;
        if(Input.GetMouseButtonDown(0))
        {
            if(attackTimer <= 0)
            {
                gun.Shoot();
                attackTimer = attackCooldown;
            }            
        }
    }
    private void Animate(float x, float z)
    {
        if(x != 0 || z != 0)
        {
            animator.SetBool("Move", true);
        }
        else
        {
            animator.SetBool("Move", false);
        }
    }
}
