using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMoveToPlayer : MonoBehaviour
{
    [SerializeField] float mouseSensitivity = 250;
    private Transform parent;
    void Start()
    {
        parent = transform.parent;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        Rotate();
    }

    void Rotate()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        parent.Rotate(Vector3.up, mouseX);
    }
}