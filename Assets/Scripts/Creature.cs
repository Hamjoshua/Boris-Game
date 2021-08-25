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
            }
        }        
    }

    public void MoveOn(Vector3 vectorMovement)
    {
        vectorMovement.y = -1;
        vectorMovement = transform.TransformDirection(vectorMovement);
        characterController.Move(vectorMovement * Time.deltaTime * speed);
    }
    
    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {            
            died = true;
            MakePhysical();
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
    }

    void RandomBoneCrush()
    {
        rb_Ar[Random.Range(0, rb_Ar.Length)].AddForce(Vector3.up * 10f);
    }
}
