using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private GameObject player;
    private PlayerController playerCntrl;

    private void Start()
    {
        player = PlayerSingleton.instance.player;
        playerCntrl = player.GetComponent<PlayerController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Obstacles"))
        {
            DestroyProjectile();
            if (other.CompareTag("Player"))
            {
                playerCntrl.ProjectileImpact();
            }
        }
    }

    void DestroyProjectile()
    {
        Destroy(gameObject);
    }
}