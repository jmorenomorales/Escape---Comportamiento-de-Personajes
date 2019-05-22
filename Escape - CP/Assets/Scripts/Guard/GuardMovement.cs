using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GuardMovement : MonoBehaviour
{
    public float speed;                 // Velocidad a la que el enemigo avanza
    public float stoppingDistance;      // Medida a la que el enemigo para
    public float retreatDistance;       // Medida en la que el enemigo se echa para atrás
    public float startTimeBtwShots;
    public GameObject projectile;
    public float viewRadius;
    public float wanderRadius;
    public float wanderTimer;
    [Range(0,360)]
    public float viewAngle;

    public LayerMask playerMask;
    public LayerMask obstacleMask;

    private float timeBtwShots, tempSpeed, timer;
    private NavMeshAgent agent;

    void Start()
    {
        timeBtwShots = startTimeBtwShots;
        agent = GetComponent<NavMeshAgent>();
        timer = wanderTimer;
    }

    public bool FindVisiblePlayer(Transform player)
    {
        float dstToTarget = Vector3.Distance(transform.position, player.position);
        if (dstToTarget < viewRadius)
        {
            // El jugador está en el radio de visión/audición
            Vector3 dirToTarget = (player.position - transform.position).normalized;
            if(Vector3.Angle(transform.forward, dirToTarget) < viewAngle / 2)
            {
                // El jugador está en el ángulo de visión
                if(!Physics.Raycast(transform.position, dirToTarget, dstToTarget, obstacleMask))
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

    public Vector3 DirFromAngle(float angleInDegrees, bool angleIsGlobal)
    {
        if (!angleIsGlobal)
        {
            angleInDegrees += transform.eulerAngles.y;
        }
        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }

    public static Vector3 RandomNavSphere(Vector3 origin, float dist, int layermask)
    {
        Vector3 randDirection = Random.insideUnitSphere * dist;

        randDirection += origin;

        NavMeshHit navHit;

        NavMesh.SamplePosition(randDirection, out navHit, dist, layermask);

        return navHit.position;
    }

    public void WanderingGuard()
    {
        timer += Time.deltaTime;

        if (timer >= wanderTimer)
        {
            Vector3 newPos = RandomNavSphere(transform.position, wanderRadius, -1);
            agent.SetDestination(newPos);
            timer = 0;
        }
    }

    public bool INeedApproach(Transform player)
    {
        return (Vector3.Distance(transform.position, player.position) > stoppingDistance);
    }

    public void Approach(Transform player)
    {
        // Moverse a la posición del jugador
        transform.position = Vector3.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
    }

    public bool IAmAtGoodDistance(Transform player)
    {
        return (Vector3.Distance(transform.position, player.position) < stoppingDistance && Vector3.Distance(transform.position, player.position) > retreatDistance);
    }
    
    public void GoodDistance(Transform player)
    {
        // Stop moving
        transform.position = this.transform.position;
    }

    public bool PlayerIsTooClose(Transform player)
    {
        return (Vector3.Distance(transform.position, player.position) < retreatDistance);
    }

    public void GoBack(Transform player)
    {
        tempSpeed = 20;
        transform.position = Vector3.MoveTowards(transform.position, player.position, -tempSpeed * Time.deltaTime);
    }

    public void Shoot(Transform player)
    {
        transform.LookAt(player);
        if (timeBtwShots <= 0)
        {
            GameObject bullet = Instantiate(projectile, transform.position, Quaternion.identity);
            bullet.GetComponent<Rigidbody>().AddForce((transform.forward) * 1000);
            timeBtwShots = startTimeBtwShots;
        }
        else
        {
            timeBtwShots -= Time.deltaTime;
        }
    }
}