using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardController : MonoBehaviour
{
    public float speed;                 // Velocidad a la que el enemigo avanza
    public float stoppingDistance;      // Medida a la que el enemigo para
    public float retreatDistance;       // Medida en la que el enemigo se echa para atrás
    public float viewDistance;          // Distancia de visión
    public float startTimeBtwShots;
    public GameObject projectile;
    public float viewRadius;
    [Range(0,360)]
    public float viewAngle;

    public LayerMask playerMask;
    public LayerMask obstacleMask;

    private Transform player;
    private float timeBtwShots, tempSpeed;

    void Start()
    {
        player = PlayerSingleton.instance.player.transform;

        timeBtwShots = startTimeBtwShots;
    }
    
    void Update()
    {
        // SE HARÍA AQUÍ LA COMPROBACIÓN DEL CAMPO DE VISIÓN
        if(FindVisiblePlayer())
        {
            //ESTÁ A LA VISTA
            transform.LookAt(player);
            if (Vector3.Distance(transform.position, player.position) > stoppingDistance)
            {
                // Moverse a la posición del jugador
                transform.position = Vector3.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
            }
            else if (Vector3.Distance(transform.position, player.position) < stoppingDistance && Vector3.Distance(transform.position, player.position) > retreatDistance)
            {
                // Stop moving
                transform.position = this.transform.position;
            }
            else if (Vector3.Distance(transform.position, player.position) < retreatDistance)
            {
                tempSpeed = 10;
                transform.position = Vector3.MoveTowards(transform.position, player.position, -tempSpeed * Time.deltaTime);
            }

            if (timeBtwShots <= 0)
            {
                Instantiate(projectile, transform.position, Quaternion.identity);
                timeBtwShots = startTimeBtwShots;
            }
            else
            {
                timeBtwShots -= Time.deltaTime;
            }
        }
        else
        {
            //FUERA DE ALCANCE - PATRULLA
            Debug.Log("Patrulla");
            transform.position = this.transform.position;
        }
    }

    private bool FindVisiblePlayer()
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
}