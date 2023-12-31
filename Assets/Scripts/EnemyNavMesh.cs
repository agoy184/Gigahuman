using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class EnemyNavMesh : MonoBehaviour
{

    [SerializeField] private Transform movePositionTransform;
    private NavMeshAgent navMeshAgent;

    private float speed = 3.5f;

    private bool isStunned = false;
    private float stunTime;

    private bool isSlowed = false;
    private float slowTime;

    private void Awake()
    {
       navMeshAgent= GetComponent<NavMeshAgent>();
    }

    public void AssignTarget(Transform target)
    {
        movePositionTransform = target;
        navMeshAgent.destination = movePositionTransform.position;
    }

    Transform getTarget()
    {
        return movePositionTransform;
    }

    void Update()
    {
        // If stunned, do nothing. While stunned, other timers (like slow) are not decremented.

        if (isStunned) {
            StunHandler();
            return;
        }

        if (isSlowed) {
            SlowHandler();
        }

        AssignTarget(GameManager.Instance.GetPlayer().transform);
    }

    void StunHandler()
    {
        if (stunTime > 0) {
            stunTime -= Time.deltaTime;
        }
        else {    
            isStunned = false;
            AssignTarget(GameManager.Instance.GetPlayer().transform);
        }
    }

    public void Stun(float duration)
    {
        isStunned = true;
        AssignTarget(transform);
        stunTime = duration;
    }

    void SlowHandler()
    {
        if (slowTime > 0) {
            slowTime -= Time.deltaTime;
        }
        else {
            isSlowed = false;
            navMeshAgent.speed = speed;
        }
    }

    public void Slow(float duration, float slowMultiplier)
    {
        isSlowed = true;
        navMeshAgent.speed = speed * slowMultiplier;
        slowTime = duration;
    }

    public float GetSpeed()
    {
        return speed;
    }
}
