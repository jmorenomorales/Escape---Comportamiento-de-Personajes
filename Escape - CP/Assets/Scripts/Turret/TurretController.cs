using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretController : MonoBehaviour
{
    private enum TEstado { ESPERANDO, GIRANDO, FIJADO_DISPARO };
    private TEstado estado = TEstado.ESPERANDO;
    private Vector3 posicionObjetivo = Vector3.zero;

    public float viewRadius;
    [Range(0, 360)]
    public float viewAngle;
    public float listeningRadius, startTimeBtwShots;
    public GameObject projectile;
    public LayerMask playerMask, obstacleMask;

    private float timeBtwShots;
    private Transform player;
    private Animator turretAnim;

    // Start is called before the first frame update
    void Start()
    {
        player = PlayerSingleton.instance.player.transform;
        turretAnim = GetComponent<Animator>();

        timeBtwShots = startTimeBtwShots;
    }

    // Update is called once per frame
    void Update()
    {
        // Si está en el radio de "AUDICIÓN"
        if (Vector3.Distance(transform.position, player.position) < listeningRadius)
        {
            // Si está a la vista
            if (FindVisiblePlayer())
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
            // Si no simplemente rota
            else
            {
                turretAnim.enabled = true;
                turretAnim.Play("TurretTurn");
            }
        }
        else
        {
            turretAnim.enabled = false;
        }
    }

    private bool FindVisiblePlayer()
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
}
