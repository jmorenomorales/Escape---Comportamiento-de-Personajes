using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunnerController : MonoBehaviour
{
    private Transform player;
    private enum TState { GO_FINALPOS, CHASE, ATTACK, GO_STARTINGPOS };
    private TState state = TState.GO_FINALPOS;

    private RunnerMovement runnerMovement;
    private PlayerController playerController;

    // Start is called before the first frame update
    void Start()
    {
        runnerMovement = gameObject.GetComponent<RunnerMovement>();

        player = PlayerSingleton.instance.player.transform;
        playerController = player.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        FSMRunnerBehaviour();
    }

    void FSMRunnerBehaviour()
    {
        switch (state)
        {
            case TState.GO_FINALPOS:
                //Debug.Log(state);
                if (runnerMovement.FindVisiblePlayer(player) || runnerMovement.PlayerIsAudible(player))
                {
                    state = TState.CHASE;
                    runnerMovement.Chase(player);
                }
                else if (runnerMovement.HasReachedFinalPos())
                {
                    state = TState.GO_STARTINGPOS;
                    runnerMovement.GoStartingPos();
                }
                else
                {
                    runnerMovement.GoFinalPos();
                }
                break;
            case TState.CHASE:
                //Debug.Log(state);
                if (runnerMovement.IsInAttackRange(player))
                {
                    state = TState.ATTACK;
                    // Metodo de atacar
                }
                else if(!runnerMovement.FindVisiblePlayer(player) && !runnerMovement.PlayerIsAudible(player))
                {
                    state = TState.GO_FINALPOS;
                    runnerMovement.GoFinalPos();
                }
                else
                {
                    runnerMovement.Chase(player);
                }
                break;
            case TState.ATTACK:
                //Debug.Log(state);
                if (!runnerMovement.IsInAttackRange(player) && (runnerMovement.FindVisiblePlayer(player) || runnerMovement.PlayerIsAudible(player)))
                {
                    state = TState.CHASE;
                    runnerMovement.Chase(player);
                }
                else if (!runnerMovement.FindVisiblePlayer(player) && !runnerMovement.PlayerIsAudible(player))
                {
                    state = TState.GO_FINALPOS;
                    runnerMovement.GoFinalPos();
                }
                else
                {
                    // Metodo de atacar
                    playerController.AttackImpact();
                }
                break;
            case TState.GO_STARTINGPOS:
                //Debug.Log(state);
                if (runnerMovement.FindVisiblePlayer(player) || runnerMovement.PlayerIsAudible(player))
                {
                    state = TState.CHASE;
                    runnerMovement.Chase(player);
                }
                else if (runnerMovement.HasReachedStartingPos())
                {
                    state = TState.GO_FINALPOS;
                    runnerMovement.GoFinalPos();
                }
                else
                {
                    runnerMovement.GoStartingPos();
                }
                break;
            default:
                break;
        }
    }
}
