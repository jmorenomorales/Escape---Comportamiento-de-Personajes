using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretController : MonoBehaviour
{
    private Transform player;

    private enum TState { OFF, ROTATING, SHOOTING };
    private TState state = TState.OFF;

    private TurretMovement turretMovement;
    private PlayerController playerController;

    void Start()
    {
        turretMovement = gameObject.GetComponent<TurretMovement>();

        player = PlayerSingleton.instance.player.transform;
        playerController = player.GetComponent<PlayerController>();
    }

    void Update()
    {
        FSMTurretBehaviour();
    }

    void FSMTurretBehaviour()
    {
        switch (state)
        {
            case TState.OFF:
                if (turretMovement.PlayerIsAudible(player))
                {
                    state = TState.ROTATING;
                    turretMovement.TurretRotate();
                }
                else
                {
                    turretMovement.TurnOff();
                }
                break;
            case TState.ROTATING:
                if (turretMovement.FindVisiblePlayer(player))
                {
                    state = TState.SHOOTING;
                    turretMovement.ShootPlayer(player);
                }
                else if(!turretMovement.PlayerIsAudible(player))
                {
                    state = TState.OFF;
                    turretMovement.TurnOff();
                }
                else
                {
                    turretMovement.TurretRotate();
                }
                break;
            case TState.SHOOTING:
                if (!turretMovement.FindVisiblePlayer(player))
                {
                    state = TState.ROTATING;
                    turretMovement.TurretRotate();
                }
                else
                {
                    turretMovement.ShootPlayer(player);
                }
                break;
            default:
                break;
        }
    }
}
