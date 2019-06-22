using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RunnerMovement : MonoBehaviour
{
    public int steps;
    public int wanderSpeed;
    public int chaseSpeed;
    public float viewRadius;
    [Range(0, 360)]
    public float viewAngle;
    public float listeningRadius;
    public LayerMask playerMask, obstacleMask;

    private Vector3 startingPos, endPos;
    private NavMeshAgent agent;

    // Start is called before the first frame update
    void Start()
    {
        startingPos = transform.position;
        endPos = transform.position + transform.forward * steps;

        agent = GetComponent<NavMeshAgent>();
        //agent.speed = 10;
    }

    public bool FindVisiblePlayer(Transform player)
    {
        float dstToTarget = Vector3.Distance(transform.position, player.position);
        if (dstToTarget < viewRadius)
        {
            // El jugador está en el radio de visión/audición
            Vector3 dirToTarget = (player.position - transform.position).normalized;
            if (Vector3.Angle(transform.forward, dirToTarget) < viewAngle / 2)
            {
                // El jugador está en el ángulo de visión
                if (!Physics.Raycast(transform.position, dirToTarget, dstToTarget, obstacleMask))
                {
                    // No hay obstáculos de por medio y por eso ve
                    return true;
                }
            }
            return false;
        }
        else
        {
            return false;
        }
    }

    public bool HasReachedFinalPos()
    {
        return (Equals(transform.position.x, endPos.x));
    }

    public void Chase(Transform player)
    {
        // Moverse a la posición del jugador
        transform.LookAt(player);
        Debug.Log("Le estoy persiguiendo");
        Debug.Log(player.position);
        transform.position = Vector3.MoveTowards(transform.position, player.position, chaseSpeed * Time.deltaTime);
    }

    public void GoStartingPos()
    {
        //transform.position = Vector3.MoveTowards(transform.position, startingPos, 10 * Time.deltaTime);
        agent.SetDestination(startingPos);
    }

    public void GoFinalPos()
    {
        //transform.position = Vector3.MoveTowards(transform.position, endPos, 10 * Time.deltaTime);
        agent.SetDestination(endPos);
    }

    public bool IsInAttackRange(Transform player)
    {
        return (Vector3.Distance(transform.position, player.position) < 2);
    }

    public bool HasReachedStartingPos()
    {
        return (Equals(transform.position.x, startingPos.x));
    }

    public Vector3 DirFromAngle(float angleInDegrees, bool angleIsGlobal)
    {
        if (!angleIsGlobal)
        {
            angleInDegrees += transform.eulerAngles.y;
        }
        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }

    public bool PlayerIsAudible(Transform player)
    {
        return (Vector3.Distance(transform.position, player.position) < listeningRadius);
    }
}
