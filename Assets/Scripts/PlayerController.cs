using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Movement variables
    Vector3 movement;
    public float speed = 5f;

    // Body variables
    private Rigidbody rb;
    private GameObject mainBody;
    private MeshRenderer meshRenderer;

    private ParticleSystem ps;

    // original material color
    private Color defaultColor;

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

        mainBody = transform.Find("Rig_Body_Upper").Find("UpperBody").gameObject;
        meshRenderer = mainBody.GetComponent<MeshRenderer>();
        defaultColor = meshRenderer.material.color;

        ps = GetComponentInChildren<ParticleSystem>();

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

        if (Input.GetKeyDown(KeyCode.LeftShift)) {
            // boost forward slightly
            transform.position += transform.forward * 0.5f;
            ps.Play();
        }

        // if shift is pressed, move faster
        if (Input.GetKey(KeyCode.LeftShift)) {
            speed = 9f;
        }
        else {
            speed = 5f;
            if (ps.isPlaying) {
                ps.Stop();
            }
        }

        if (Input.GetKey(KeyCode.A)) horizontal = -1;
        else if (Input.GetKey(KeyCode.D)) horizontal = 1;
        if (Input.GetKey(KeyCode.W)) vertical = 1;
        else if (Input.GetKey(KeyCode.S)) vertical = -1;

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

        // play random hurt sound from 1 to 3
        AudioManager.Instance.PlaySound("MetalHit" + Random.Range(1, 4));
        
        hp -= damage;
        Debug.Log("Player took " + damage + " damage. HP: " + hp);

        meshRenderer.material.color = Color.red;
        Invoke("ResetColor", 0.2f);
        
        if (hp <= 0)
        {
            // TODO: Game over
        }
        MakeInvincible(duration);
    }

    void ResetColor() {
        meshRenderer.material.color = defaultColor;
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
