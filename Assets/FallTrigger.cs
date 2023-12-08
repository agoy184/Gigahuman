using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallTrigger : MonoBehaviour
{
    void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Collider>().gameObject.tag == "Player")
        {
            GameManager.Instance.Die();
            Debug.Log("HI!");
        }
    }

}
