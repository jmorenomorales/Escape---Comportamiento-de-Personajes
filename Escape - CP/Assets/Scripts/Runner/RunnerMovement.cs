using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RunnerMovement : MonoBehaviour
{
    public int steps;

    private Vector3 startingPos, endPos;
    private NavMeshAgent agent;

    // Start is called before the first frame update
    void Awake()
    {
        startingPos = transform.position;
        endPos = transform.position + transform.forward * steps;
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Equals(transform.position.x, startingPos.x))
        {
            Debug.Log("Entro en el primer if");
            agent.SetDestination(endPos);
        }
        else if(Equals(transform.position.x, endPos.x))
        {
            Debug.Log("Entro en el segundo if");
            agent.SetDestination(startingPos);
        }

        //Debug.Log("Transform.position: " + transform.position + ", startingPos: " + startingPos);
    }
}
