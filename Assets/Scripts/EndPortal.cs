using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndPortal : MonoBehaviour
{
    public string sceneToLoad;
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("TRIGGERED");
            GameManager.Instance.TravelToParallelDimension(sceneToLoad);
        }
    }
}
