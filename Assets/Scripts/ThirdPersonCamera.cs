using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    void Start()
    {      
        // set position relative to player
        transform.position = new Vector3(0, 3, -6);
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
