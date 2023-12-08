using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalManager : MonoBehaviour
{
    public GameObject NdPortal;
    void Update()
    {

        if (ArePortalsDisabled())
        {
            if (!NdPortal.activeSelf) {
                AudioManager.Instance.PlaySound("PortalSpawn");
                // Enable the script on the current object
                NdPortal.SetActive(true);
            } else {
                if (!AudioManager.Instance.IsMusicPlaying("Portal Idle") && !GameManager.Instance.isParallelDimension) {
                    AudioManager.Instance.PlayMusic("Portal Idle");
                }
            }
        }
        else
        {
            // Disable the script on the current object
            NdPortal.SetActive(false);
            
        }

        // Your other update logic goes here
    }

    bool ArePortalsDisabled()
    {
        GameObject[] portals = GameObject.FindGameObjectsWithTag("Portal");

        foreach (GameObject portal in portals)
        {
            if (portal.activeSelf)
            {
                // At least one portal is still active, return false
                return false;
            }
        }

        // All portals are disabled, return true
        return true;
    }
}