using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalManager : MonoBehaviour
{
    // portal singleton
    private static PortalManager _instance;
    public static PortalManager Instance { get { return _instance; } }

    private void Awake()
    {
        _instance = this;
    }

    public GameObject NdPortal;

    public GameObject[] portals;

    private AudioSource audioSource;

    private bool Victory = false;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void ArePortalsDisabled()
    {
        foreach (GameObject portal in portals)
        {
            if (portal.activeSelf)
                return;
        }
        
        TriggerCheck();
    }

    void TriggerCheck() {
        if (!GameManager.Instance.isParallelDimension) {
            if (!NdPortal.activeSelf) {
                AudioManager.Instance.PlaySound("PortalSpawn", audioSource);
                // Enable the script on the current object
                NdPortal.SetActive(true);
                if (!AudioManager.Instance.IsMusicPlaying("Portal Idle") && !GameManager.Instance.isParallelDimension) {
                    AudioManager.Instance.PlayMusic("Portal Idle");
                }
            } 
        } else {
            GameManager.Instance.GameOver(true);
        }
    }
}