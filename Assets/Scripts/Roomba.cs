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


    private Rigidbody rbody;
    

    // Start is called before the first frame update
    void Start()
    {
        rbody = GetComponent<Rigidbody>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.speed = roombaSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        
        // turn to position of player
        if(player != null)
        {
            navMeshAgent.SetDestination(player.position);

            //rbody.MoveRotation()

            //rbody.MovePosition(Vector3.LerpUnclamped(this.transform.position, player.transform.position, roombaSpeed * Time.deltaTime));
            //rbody.MovePosition(player.transform.position * Time.deltaTime * roombaSpeed);

        }

    }

    private void OnTriggerEnter(Collider c)
    {
        // lose game if player contacts roomba
        if (c.gameObject.CompareTag("Player"))
        {
            Time.timeScale = 0f;
            Debug.Log("Game Over");
        }
    }

}
