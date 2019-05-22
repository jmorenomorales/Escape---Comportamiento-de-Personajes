using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretMovement : MonoBehaviour
{
    public float viewRadius;
    [Range(0, 360)]
    public float viewAngle;
    public float listeningRadius, startTimeBtwShots;
    public GameObject projectile;
    public LayerMask playerMask, obstacleMask;

    private float timeBtwShots;
    private Animator turretAnim;

    // Start is called before the first frame update
    void Start()
    {
        turretAnim = GetComponent<Animator>();

        timeBtwShots = startTimeBtwShots;
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

    public Vector3 DirFromAngle(float angleInDegrees, bool angleIsGlobal)
    {
        if (!angleIsGlobal)
        {
            angleInDegrees += transform.eulerAngles.y;
        }
        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }

    public void TurnOff()
    {
        turretAnim.enabled = false;
    }

    public bool PlayerIsAudible(Transform player)
    {
        return (Vector3.Distance(transform.position, player.position) < listeningRadius);
    }

    public void TurretRotate()
    {
        turretAnim.enabled = true;
        turretAnim.Play("TurretTurn");
    }

    public void ShootPlayer(Transform player)
    {
        turretAnim.enabled = false;
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
