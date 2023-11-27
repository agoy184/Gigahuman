using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Vector3 movement;
    private Rigidbody rb;
    private GameObject camera;
    private GameObject head;

    private Vector2 turn;
    [SerializeField] private float sensitivity = 1f;
    private float verticalRange = 10f;

    public float speed = 2f;

    void Start()
    {
        GameManager.Instance.SetPlayer(gameObject);
        rb = GetComponent<Rigidbody>();
        camera = transform.Find("Main Camera").gameObject;
        head = transform.Find("Body").Find("Head").gameObject;
    }

    void Update()
    {
        InputHandler();
        PerspectiveHandler();
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

    void PerspectiveHandler()
    {
        turn.x += Input.GetAxis("Mouse X");
        turn.y += Input.GetAxis("Mouse Y");
        if (turn.y > verticalRange) turn.y = verticalRange;
        else if (turn.y < -verticalRange) turn.y = -verticalRange;

        transform.localRotation = Quaternion.Euler(0, turn.x * sensitivity, 0);
        camera.transform.localRotation = Quaternion.Euler(-turn.y * sensitivity, 0, 0);
        head.transform.localRotation = Quaternion.Euler(-turn.y * sensitivity, 0, 0);
    }
}
