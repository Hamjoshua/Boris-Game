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
            //todo сделать толчок с той стороны, с которой прилетела пуля
            //Vector3 collisiondetect = collision.transform.rotation;
            creature.MoveOn(new Vector3(0, 10f, -10f));

            if(!creature.died)
            {
                int damage = collision.gameObject.GetComponent<BulletLogic>().damage;
                creature.TakeDamage(damage);
                Destroy(collision.gameObject, 2);
            }
            
            //rigidbody = GetComponent<Rigidbody>();
            //rigidbody.isKinematic = false;
            //rigidbody.AddForce(collision.transform.forward * 5f);
            //Debug.Log(gameObject.name + " hitted!");
        }
    }
}
