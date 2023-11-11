using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Vector3 movement;
    Rigidbody rb;

    public float speed = 2f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        InputHandler();
    }

    void FixedUpdate()
    {
        MoveHandler();
    }

    void InputHandler()
    {
        float horizontal = 0f;
        float vertical = 0f;

        if (Input.GetKey(KeyCode.A)) horizontal = -speed;
        else if (Input.GetKey(KeyCode.D)) horizontal = speed;
        if (Input.GetKey(KeyCode.W)) vertical = speed;
        else if (Input.GetKey(KeyCode.S)) vertical = -speed;
        
        movement = new Vector3(horizontal, 0f, vertical);
    }

    void MoveHandler()
    {
        rb.MovePosition(transform.position + movement * speed * Time.deltaTime);

        if (movement != Vector3.zero)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(movement), 0.15F);
        }
    }
}
