using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    public LayerMask collisionLayer;
    public GameObject playerBody;
    private Vector3 originalPosition = new Vector3(3, 3, -6);
    void Awake()
    {      
        // set position relative to player
        transform.position = originalPosition;
    }

    void Update()
    {
        HandleCameraCollision();

        // On escape, unlock or lock the cursor
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Cursor.lockState == CursorLockMode.Locked)
                Cursor.lockState = CursorLockMode.None;   
            else
                Cursor.lockState = CursorLockMode.Locked;   
        }
    }

    void HandleCameraCollision()
    {
        RaycastHit hit;
        
        Vector3 desiredPosition = playerBody.transform.TransformPoint(originalPosition);

        // Check for collisions between the camera and the player or obstacles
        if (Physics.Linecast(playerBody.transform.position, desiredPosition, out hit, collisionLayer))
        {
            // Adjust the camera position to avoid the obstacle
            transform.position = hit.point;
        }
        else
        {
            // Move the camera to the desired position if no collision
            transform.position = desiredPosition;
        }
    }
}
