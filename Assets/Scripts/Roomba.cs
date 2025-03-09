using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.AI;

// Roomba state machine
//   Since roomba movement will be broken up, creating a state machine instead of having a bunch of booleans
public enum RoombaState
{
    Idle,
    FollowRoute, 
    PlayerInSights,
    ChasePlayer,
    ReadyToCharge,
    Charge,
    PowerOff
}

[RequireComponent(typeof(Animator), typeof(Rigidbody))]
// this file depicts the behavior of the basic roomba enemy
// this enemy is made to always track the player obect
// if it hits the player, the game is over
public class Roomba : MonoBehaviour
{
    protected RoombaState roombaState; // state machine to keep track of roomba behavior

    public Transform player; // roomba target
    public float roombaSpeed = 1f; // speed of roomba

    protected NavMeshAgent navMeshAgent;
    protected Animator anim;
    protected Rigidbody rbody;

    // waypoints for when its not chasing the player
    public GameObject[] waypoints;
    private int currWaypoint = -1; // no waypoint at moment

    // Start is called before the first frame update
    void Start()
    {
        roombaState = RoombaState.FollowRoute;

        anim = GetComponent<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        rbody = GetComponent<Rigidbody>();

        navMeshAgent.speed = roombaSpeed;

        // Using for FollowRoute case because they seem to stop when they don't
        // land at exact target: https://docs.unity3d.com/540/Documentation/ScriptReference/NavMeshAgent-stoppingDistance.html
        navMeshAgent.stoppingDistance = 0.5f;

        if (waypoints.Length > 0)
        {
            // set waypoint to first one
            setNextWaypoint();
        }
    }

    // Update is called once per frame
    void Update()
    {
        switch (roombaState)
        {
            // if you have a set of waypoints, go follow those
            case RoombaState.FollowRoute:

                // route exists in the first place
                if (currWaypoint > -1)
                {
                    // waypoint reached and ai not bugging out
                    //if (navMeshAgent.remainingDistance <= 0 && navMeshAgent.pathPending == false)
                    // DG: See here for reason to change: https://docs.unity3d.com/540/Documentation/ScriptReference/NavMeshAgent-stoppingDistance.html
                    if (!navMeshAgent.pathPending && navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance)
                    {
                        setNextWaypoint();
                    }
                }

                break;

            // track player movement
            case RoombaState.PlayerInSights:
                if (player != null)
                {                
                    // DG: Set y to 0 bc Roomba has taken off for flight
                    Vector3 direction = player.position - transform.position;
                    // direction.y = 0;

                    rbody.MoveRotation(Quaternion.LerpUnclamped(transform.rotation,
                        Quaternion.LookRotation(direction, Vector3.up),
                        Time.deltaTime * roombaSpeed));
                    
                    /*rbody.MoveRotation(Quaternion.LerpUnclamped(transform.rotation,
                        Quaternion.Inverse(Quaternion.LookRotation((player.position - transform.position), Vector3.up)),
                        1.0f));*/
                    

                    //rbody.MovePosition(Vector3.LerpUnclamped(this.transform.position, player.transform.position, roombaSpeed * Time.deltaTime));
                    //rbody.MovePosition(player.transform.position * Time.deltaTime * roombaSpeed);

                }
                break;

            // follow the player 
            case RoombaState.ChasePlayer:
                if (player != null)
                {
                    navMeshAgent.SetDestination(player.position);
                }
                break;

            // put a spot for deactivate roombas just in case
            case RoombaState.PowerOff:
                break;

            default:
                break;
        }
    }

    private void OnTriggerEnter(Collider c)
    {
    }

    public bool IsPowerOn()
    {
        // Debug.Log(roombaState);
        return !(roombaState == RoombaState.PowerOff);
    }

    public virtual void TurnRoombaOff()
    {
        // Debug.Log("Roomba turned off");
        roombaState = RoombaState.PowerOff;
        navMeshAgent.isStopped = true;
        rbody.freezeRotation = true;

        anim.Play("RoombaOff");
    }

    // player in range that starts alerting roomba
    public void Warning()
    {
        // stop what we're doing there's a person
        // Debug.Log("Roomba detected a human");
        navMeshAgent.SetDestination( rbody.position );
        roombaState = RoombaState.PlayerInSights;
        rbody.freezeRotation = true;
        Debug.Log("Player in sights");

        anim.SetBool("Alert", true);
    }

    public void OffWarning()
    {
        // Debug.Log("Roomba gave up");
        roombaState = RoombaState.FollowRoute;

        anim.SetBool("Alert", false);
    }

    // player in range to be chased by roomba
    public virtual void ChasePlayer()
    {
        // Debug.Log("Roomba detected IS RUNNINGG AFTER YOU!");
        navMeshAgent.SetDestination(player.position);
        roombaState = RoombaState.ChasePlayer;

        Debug.Log("Chasing Player");

        anim.SetBool("Chasing", true);
    }

    public virtual void OffChasePlayer()
    {
        // Debug.Log("MUST HAVE BEEN THE WIND");
        navMeshAgent.SetDestination(rbody.position);
        roombaState = RoombaState.PlayerInSights;

        anim.SetBool("Chasing", false);
    }

    // create next waypoint
    private void setNextWaypoint()
    {

        // loop back to 0 if list out of bounds
        if (currWaypoint >= waypoints.Length - 1)
        {
            currWaypoint = 0;
        }

        // go to next waypoint
        else
        {
            currWaypoint++;
        }

        navMeshAgent.SetDestination(waypoints[currWaypoint].transform.position);
        //Debug.Log(currWaypoint);
    }

}
