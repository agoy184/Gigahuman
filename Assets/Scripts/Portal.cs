using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "PortalBlocker")
        {
            AudioManager.Instance.PlaySound("PortalDestroy");
            other.gameObject.GetComponent<ExplodableMonitor>().DestroyShards();
            // shrink both portal and blocker
            StartCoroutine(Shrink(other.gameObject));
            StartCoroutine(Shrink(gameObject));
        }
    }

    IEnumerator Shrink(GameObject other)
    {
        float time = 0f;
        while (time < 1f)
        {
            time += Time.deltaTime;
            transform.localScale = Vector3.Lerp(transform.localScale, Vector3.zero, time);
            other.transform.localScale = Vector3.Lerp(other.transform.localScale, Vector3.zero, time);
            yield return null;
        }
        Destroy(other);
    }
}
