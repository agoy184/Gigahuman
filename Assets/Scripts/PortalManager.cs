using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalManager : MonoBehaviour
{
    public GameObject NdPortal;

    bool Victory = false;
    void Update()
    {

        if (ArePortalsDisabled())
        {
            if (!GameManager.Instance.isParallelDimension) {
                if (!NdPortal.activeSelf) {
                    AudioManager.Instance.PlaySound("PortalSpawn");
                    // Enable the script on the current object
                    NdPortal.SetActive(true);
                } else {
                    if (!AudioManager.Instance.IsMusicPlaying("Portal Idle") && !GameManager.Instance.isParallelDimension) {
                        AudioManager.Instance.PlayMusic("Portal Idle");
                    }
                }
            } else {
                if (!Victory) {
                    AudioManager.Instance.StopMusic();
                    AudioManager.Instance.PlaySound("Victory");
                    Victory = true;
                }
            
            }
        }
        else
        {
            if (NdPortal != null) {
                if (NdPortal.activeSelf) {
                    NdPortal.SetActive(false);
                }
            }
            
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