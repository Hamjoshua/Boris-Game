using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteEnemy : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        Creature creature = collision.gameObject.GetComponent<Creature>();
        if(creature)
        {
            Debug.Log("Сваленный зомби уничтожен");
            creature.died = false;
        }
    }
}
