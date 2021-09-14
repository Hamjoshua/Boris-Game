using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMoveToPlayer : MonoBehaviour
{
    [SerializeField] float mouseSensitivity = 250;
    public Transform player;
    public float distanceZ = 30f;   
    void Start()
    {        
        //Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Rotate();
        MoveToPlayer();
    }

    void Rotate()
    {
        Vector2 center = new Vector2(0.5f, 0.5f);
        Vector2 mpos = Input.mousePosition;
        Vector2 res = Camera.main.ScreenToViewportPoint(mpos);
        float angle = Mathf.Atan2(res.y - center.y, res.x - center.x);
        angle = angle * (180 / 3.14f);        
        player.eulerAngles = new Vector3(0, 90 - angle, 0);       
    }

    void MoveToPlayer()
    {
        Vector3 newPos = new Vector3(player.position.x, transform.position.y, player.position.z - distanceZ * 2);
        transform.position = Vector3.MoveTowards(transform.position, newPos, 1000);
    }
}
