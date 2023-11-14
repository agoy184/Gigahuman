using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Vector3 movement;
    Rigidbody rb;
    [SerializeField] private GameObject camera;
    [SerializeField] private GameObject pusher;

    public float speed = 2f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        camera = transform.Find("Main Camera").gameObject;
        pusher = transform.Find("Pusher").gameObject;
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

        transform.rotation = camera.transform.rotation;
        
        movement = transform.forward * vertical + transform.right * horizontal;
    }

    void MoveHandler()
    {
        rb.MovePosition(transform.position + movement * speed * Time.deltaTime);
    }
}
