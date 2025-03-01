using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.AI;

// this file depicts the behavior of the basic roomba enemy
// this enemy is made to always track the player obect
// if it hits the player, the game is over
public class Roomba : MonoBehaviour
{
    public Transform player; // roomba target
    public float roombaSpeed = 1f; // speed of roomba

    private NavMeshAgent navMeshAgent;
    private Animator anim;
    private Rigidbody rbody;

    private bool isOn;
    private bool playerInSights = false;
    private bool playerChasing = false;
    

    // Start is called before the first frame update
    void Start()
    {
        //rbody = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        rbody = GetComponent<Rigidbody>();

        navMeshAgent.speed = roombaSpeed;

        isOn = true;
    }

    // Update is called once per frame
    void Update()
    {
     
        // must be on before doing anything
        if (playerChasing && IsPowerOn())
        {
            // turn to position of player
            if (player != null)
            {
                navMeshAgent.SetDestination(player.position);

                //rbody.MoveRotation()

                //rbody.MovePosition(Vector3.LerpUnclamped(this.transform.position, player.transform.position, roombaSpeed * Time.deltaTime));
                //rbody.MovePosition(player.transform.position * Time.deltaTime * roombaSpeed);

            }
        }

        // stay still but kep watching the player
        else if (playerInSights && IsPowerOn())
        {
            // blep
        }

    }

    private void OnTriggerEnter(Collider c)
    {
        // lose game if player contacts roomba
        /*if (c.gameObject.CompareTag("Player"))
        {
            Time.timeScale = 0f;
            Debug.Log("Game Over");
        }*/


    }

    public bool IsPowerOn()
    {
        return isOn;
    }

    public void TurnRoombaOff()
    {
        Debug.Log("Roomba turned off");
        isOn = false;

        //anim.SetBool("PowerOn", false);
        anim.Play("RoombaOff");
    }

    public void Warning()
    {
        // stop what we're doing there's a person
        Debug.Log("Roomba detected a human");
        navMeshAgent.SetDestination( rbody.position );
        playerInSights = true;

        anim.SetBool("Alert", true);
    }

    public void ChasePlayer()
    {
        Debug.Log("Roomba detected IS RUNNINGG AFTER YOU!");
        navMeshAgent.SetDestination(player.position);
        playerChasing = true;

        anim.SetBool("Chasing", true);
    }

    public void OffWarning()
    {
        Debug.Log("Roomba gave up");
        playerInSights = false;

        anim.SetBool("Alert", false);
    }

    public void OffChasePlayer()
    {
        Debug.Log("MUST HAVE BEEN THE WIND");
        navMeshAgent.SetDestination(rbody.position);
        playerChasing = false;

        anim.SetBool("Chasing", false);
    }

}
