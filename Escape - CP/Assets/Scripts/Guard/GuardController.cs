using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardController : MonoBehaviour
{
    private Transform player;

    private enum TState { WANDERING, PLAYER_APPROACH, KEEP_DISTANCE_PLAYER, GO_BACK_PLAYER};
    private TState state = TState.WANDERING;

    private GuardMovement guardMovement;
    private PlayerController playerController;

    // Start is called before the first frame update
    void Start()
    {
        guardMovement = gameObject.GetComponent<GuardMovement>();

        player = PlayerSingleton.instance.player.transform;
        playerController = player.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        FSMGuardBehaviour();
    }

    void FSMGuardBehaviour()
    {
        switch (state)
        {
            case TState.WANDERING:
                if (guardMovement.FindVisiblePlayer(player))
                {
                    state = TState.PLAYER_APPROACH;
                    guardMovement.Approach(player);
                }
                else
                {
                    guardMovement.WanderingGuard();
                }
                break;

            case TState.PLAYER_APPROACH:
                guardMovement.Shoot(player);
                if (!guardMovement.INeedApproach(player))
                {
                    state = TState.KEEP_DISTANCE_PLAYER;
                    guardMovement.GoodDistance(player);
                }
                else if(!guardMovement.FindVisiblePlayer(player))
                {
                    state = TState.WANDERING;
                    guardMovement.WanderingGuard();
                }
                else
                {
                    guardMovement.Approach(player);
                }
                break;

            case TState.KEEP_DISTANCE_PLAYER:
                guardMovement.Shoot(player);
                if (guardMovement.INeedApproach(player))
                {
                    state = TState.PLAYER_APPROACH;
                    guardMovement.Approach(player);
                }
                else if (guardMovement.PlayerIsTooClose(player))
                {
                    state = TState.GO_BACK_PLAYER;
                    guardMovement.GoBack(player);
                }
                break;

            case TState.GO_BACK_PLAYER:
                guardMovement.Shoot(player);
                if (guardMovement.IAmAtGoodDistance(player))
                {
                    state = TState.KEEP_DISTANCE_PLAYER;
                    guardMovement.GoodDistance(player);
                }
                else
                {
                    guardMovement.GoBack(player);
                }
                break;

            default:
                break;
        }
    }
}