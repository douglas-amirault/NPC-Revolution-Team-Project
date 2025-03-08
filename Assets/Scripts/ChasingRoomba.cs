using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.AI;

[RequireComponent(typeof(Animator), typeof(Rigidbody))]

// subset of roomba: won't give warning but no waypoints and has extra detection for 
public class ChasingRoomba : Roomba
{
    // The Roombas were charging too slow when relying on animation to update
    // clip name; added these var to speed charging
    private float chargeDelay = 0.2f;
    private float chargeTimer = 0f;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        rbody = GetComponent<Rigidbody>();

        roombaState = RoombaState.Idle;
    }

    // Update is called once per frame
    void Update()
    {
        if (roombaState == RoombaState.ReadyToCharge)
        {
            // turn to player and wait for chargeup
            if (player != null)
            {
                // rbody didn't want to work so I used transform
                transform.rotation = Quaternion.LerpUnclamped(transform.rotation,
                        Quaternion.LookRotation((player.position - transform.position), Vector3.up),
                        Time.deltaTime * 2);

                // identify when roomba is ready by if it got past pre-charge animation
                // string clipName = anim.GetCurrentAnimatorClipInfo(0)[0].clip.ToString();
                // if( clipName.StartsWith("ChasingRoombaRun"))
                // {
                //     roombaState = RoombaState.Charge;
                // }
                // DG: relying on teh animator to change clip name may have led
                // to slow charging - change below is consistent with lectures
                // where we use delta time to make things indpendent of frame
                // rate. This seems to speed charging and improve gameplay.
                chargeTimer += Time.deltaTime;
                if (chargeTimer >= chargeDelay)
                {
                    roombaState = RoombaState.Charge;
                    chargeTimer = 0f;
                }
            }
        }
        else if (roombaState == RoombaState.Charge)
        {
            // DG: Added this because without it the Roomba locked its rotation
            // on player and missed. Here we continue to update rotation.
            if (player != null)
            {
                transform.rotation = Quaternion.LerpUnclamped(transform.rotation,
                    Quaternion.LookRotation((player.position - transform.position), Vector3.up),
                    Time.deltaTime * 2);
            }
            rbody.AddForce( transform.forward * roombaSpeed, ForceMode.Impulse);
        }
    }

    public override void TurnRoombaOff()
    {
        // Debug.Log("Roomba jumping turned off");
        roombaState = RoombaState.PowerOff;
        rbody.freezeRotation = true;

        anim.Play("ChasingRoombaOff");
    }

    // player in range to be chased by roomba
    public override void ChasePlayer()
    {
        if (roombaState != RoombaState.Charge)
        {
            // Debug.Log("Roomba detected IS RUNNINGG AFTER YOU CHASE!");
            roombaState = RoombaState.ReadyToCharge;

            anim.SetBool("ChargeRun", true);
        }
    }

    public override void OffChasePlayer()
    {
        // Debug.Log("Actually...ignore this state, we stop when we hit a wall or player");
    }

    // stop this funky ride when we hit a wall
    private void OnCollisionEnter(Collision collision)
    {
        if (roombaState == RoombaState.Charge)
        {
            // Debug.Log("bonk");
            roombaState = RoombaState.Idle;

            anim.SetBool("ChargeRun", false);
        }
    }
}
