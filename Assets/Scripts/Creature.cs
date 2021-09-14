using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creature : MonoBehaviour
{
    public bool died = false;
    public int health = 100;
    public float speed = 10f;
    private CharacterController characterController;
    private Rigidbody[] rb_Ar;
    public bool isLiving = true;

    void Start()
    {
        if(isLiving)
        {
            characterController = GetComponent<CharacterController>();
            rb_Ar = GetComponentsInChildren<Rigidbody>();
            foreach (Rigidbody rigidbody in rb_Ar)
            {
                rigidbody.gameObject.layer = LayerMask.NameToLayer("ragdoll_rb");                
                rigidbody.isKinematic = true;                
                rigidbody.gameObject.AddComponent(typeof(rb_OnCollision));
                rigidbody.gameObject.GetComponent<rb_OnCollision>().creature = this;
            }
        }        
    }

    public void MoveOn(Vector3 vectorMovement, int typeOfMovement=0)
    {
        if(vectorMovement.y == 0)
        {
            vectorMovement.y = -9.8f;
        }
        
        if(typeOfMovement == 0)
        {
            vectorMovement = transform.TransformDirection(vectorMovement);
        }        
        characterController.Move(vectorMovement * Time.deltaTime * speed);
    }
    
    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {            
            died = true;
            if(isLiving)
            {
                MakePhysical();
            }            
        }
    }

    void MakePhysical()
    {        
        GetComponent<Animator>().enabled = false;        
        foreach (Rigidbody rigidbody in rb_Ar)
        {
            rigidbody.isKinematic = false;
        }        
        RandomBoneCrush();
        characterController.enabled = false;
    }

    void RandomBoneCrush()
    {
        rb_Ar[Random.Range(0, rb_Ar.Length)].AddForce(Vector3.up * 100f);
    }
}
