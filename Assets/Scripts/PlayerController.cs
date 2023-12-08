using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Movement variables
    Vector3 movement;
    public float speed = 2f;

    // Body variables
    private Rigidbody rb;

    // Combat variables
    private GameObject gun;
    private Gun gunScript;
    public int hp = 100;
    public int maxHp = 100;
    private float invincibilityTime;
    public bool isInvincible = false;

    // Perspective variables
    private GameObject camera;
    private Vector2 turn;
    private float sensitivity = 0.75f;
    private float verticalRange = 15f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        camera = transform.Find("Main Camera").gameObject;
        gun = transform.Find("Body").Find("Gun").gameObject;
        gunScript = gun.GetComponent<Gun>();

        GameManager.Instance.SetPlayer(gameObject);
        DontDestroyOnLoad(gameObject);
    }

    void Update()
    {
        InputHandler();
        PerspectiveHandler();
        GunHandler();

        if (isInvincible) {
            InvinciblityHandler();
        }
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
    }

    void GunHandler()
    {
        if (Input.GetMouseButtonDown(0) && gunScript.CanFire())
        {
            gunScript.Fire();
        }
    }

    public void TakeDamage(int damage, float duration)
    {
        if (isInvincible) return;
        
        hp -= damage;
        Debug.Log("Player took " + damage + " damage. HP: " + hp);
        
        if (hp <= 0)
        {
            // TODO: Game over
        }
        MakeInvincible(duration);
    }

    void MakeInvincible(float duration)
    {
        isInvincible = true;
        invincibilityTime = duration;
    }

    void InvinciblityHandler()
    {
        if (invincibilityTime > 0) {
            invincibilityTime -= Time.deltaTime;
        }
        else {
            isInvincible = false;
        }
    }

    public GameObject GetGun()
    {
        return gun;
    }
}
