using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Start is called before the first frame update
    Creature creature;    
    private CharacterController characterController;
    public Animator animator;
    public Inventory inventory;
    
    Camera _cam;
    Vector3 targetDir;
    void Start()
    {        
        creature = GetComponent<Creature>();        
    }
    
    private void Update()
    {
        if (!creature.died)
        {
            inventory.Manage();            
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

            creature.MoveOn(movement, 1);            
            
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
