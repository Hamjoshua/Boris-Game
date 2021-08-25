using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rb_OnCollision : MonoBehaviour
{
    private Rigidbody rigidbody;
    public Creature creature;

    private void OnCollisionEnter(Collision collision)
    {        
        if (collision.gameObject.layer == LayerMask.NameToLayer("Bullet"))
        {
            //rigidbody = GetComponent<Rigidbody>();
            //rigidbody.isKinematic = false;
            //rigidbody.AddForce(collision.transform.forward * 5f);
            //Debug.Log(gameObject.name + " hitted!");
        }
    }
}
