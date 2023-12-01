using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;   
        
        // set position relative to player
        transform.position = new Vector3(0, 5, -8);
    }

    void Update()
    {
        // On escape, unlock or lock the cursor
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Cursor.lockState == CursorLockMode.Locked)
                Cursor.lockState = CursorLockMode.None;   
            else
                Cursor.lockState = CursorLockMode.Locked;   
        }
    }
}
