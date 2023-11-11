using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopDownCamera : MonoBehaviour
{
    private GameObject player;
    private float height = 10.0f;
    private float smoothSpeed = 10.0f;
    private Vector3 offset;

    private static TopDownCamera _instance;
    public static TopDownCamera Instance { get { return _instance; } }

    private void Awake()
    {
        if (_instance != null && _instance != this)
            Destroy(this.gameObject);
        else
            _instance = this;
    }

    void Start()
    {
        offset = new Vector3(0, height, 0);
        player = GameManager.Instance.GetPlayer();
        transform.position = player.transform.position + offset;

        transform.LookAt(player.transform);
    }

    void FixedUpdate()
    {
        Vector3 desiredPosition = player.transform.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
        transform.position = smoothedPosition;
    }

    public float GetHeight()
    {
        return height;
    }

    public void SetHeight(float newHeight)
    {
        height = newHeight;
        offset = new Vector3(0, height, 0);
    }

    public float GetSmoothSpeed()
    {
        return smoothSpeed;
    }

    public void SetSmoothSpeed(float newSmoothSpeed)
    {
        smoothSpeed = newSmoothSpeed;
    }

}
