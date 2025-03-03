using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent), typeof(Animator), typeof(Rigidbody))]

// subset of roomba: won't give warning but no waypoints and has extra detection for 
public class JumpingRoomba : Roomba
{

    //private NavMeshAgent navMeshAgent;
    //private Animator anim;
    //private Rigidbody rbody;

    // roomba simply chases player on trigger
    //private bool chasePlayer;
    private float colliderDist = 10f;

    // Start is called before the first frame update
    void Start()
    {

        anim = GetComponent<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        rbody = GetComponent<Rigidbody>();

        navMeshAgent.speed = roombaSpeed;

        roombaState = RoombaState.Idle;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("Please don't tell me it's updating as well.../");

        if (roombaState == RoombaState.ChasePlayer)
        {
            if (player != null)
            {
                navMeshAgent.SetDestination(player.position);
                Debug.Log("How do I raycast???");



                //Debug.Log(transform.position);
                //Debug.Log(player.position);
                bool blocked = Physics.Raycast(transform.position, player.position,
                    colliderDist);
                Debug.DrawLine(transform.position, player.position, blocked ? Color.red : Color.green);

                // detect if there is object in way
                /*
                NavMeshHit hit;
                bool blocked = NavMesh.Raycast(transform.position, player.position, out hit, NavMesh.AllAreas);
                Debug.DrawLine(transform.position, player.position, blocked? Color.red : Color.green);

                if (NavMesh.Raycast(transform.position, player.position, out hit, NavMesh.AllAreas))
                    Debug.Log("blocked");
                    Debug.DrawRay(hit.position, Vector3.up, Color.red);
                */

            }
        }
    }

    public override void TurnRoombaOff()
    {
        Debug.Log("Roomba jumping turned off");
        roombaState = RoombaState.PowerOff;
        navMeshAgent.isStopped = true;
        rbody.freezeRotation = true;



        anim.Play("JumpingRoombaOff");
    }

    // player in range to be chased by roomba
    public override void ChasePlayer()
    {
        Debug.Log("Roomba detected IS RUNNINGG AFTER YOU!");
        //navMeshAgent.SetDestination(player.position);
        roombaState = RoombaState.ChasePlayer;

        anim.SetBool("Chasing", true);
    }

    public override void OffChasePlayer()
    {
        Debug.Log("MUST HAVE BEEN THE WIND");
        //navMeshAgent.SetDestination(rbody.position);
        navMeshAgent.ResetPath();
        roombaState = RoombaState.Idle;

        anim.SetBool("Chasing", false);
    }
}
