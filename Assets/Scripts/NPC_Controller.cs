using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPC_Controller : MonoBehaviour
{
    public Transform targetLocationApproach;
    public Vector3 targetLocationLeave;
    public Transform playerPos;

    private NavMeshAgent agent;
    private Animator anim;

    private bool walk;
    private bool leave;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        targetLocationLeave = this.transform.position;
    }


    /// <summary>
    /// Mostly checking if agent has reached destination.
    /// If reached, change animation states
    /// </summary>
    private void Update()
    {
        // TODO: This should go to animator states.
        if (walk)
        {
            if (agent.remainingDistance < 0.1)
            {
                anim.SetBool("walk", false);
                transform.LookAt(playerPos);
                walk = false;
                anim.SetBool("talk", true);

            }
        }
        else if (leave)
        {
            if (agent.remainingDistance < 0.1)
            {
                anim.SetBool("leave", false);
                leave = false;
            }
        }
        
    }

    /// <summary>
    /// Initializes approach to counter 
    /// </summary>
    public void InitApproachToCounter()
    {   
        anim.SetBool("walk", true);
        agent.SetDestination(targetLocationApproach.position);
        walk = true;
    }

    /// <summary>
    /// Initializes leave from counter 
    /// </summary>
    public void InitLeave()
    {
        anim.SetBool("talk", false);
        anim.SetBool("leave", true);
        StartCoroutine(TurnAround());
    }

    /// <summary>
    /// Makes sure the NPC has played the turning animation and 
    /// has left the location before checking if new remaining distance < 0.1f
    /// </summary>
    IEnumerator TurnAround()
    {
        yield return new WaitForSeconds(0.28f);
        agent.SetDestination(targetLocationLeave);
        yield return new WaitForSeconds(0.2f);
        leave = true;
    }
}
