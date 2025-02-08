using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControls : MonoBehaviour
{
    // toggle for debug purposes
    public Camera overhead;
    private Camera playerCam;
    private bool overheadOn;

    public GameObject player;
    private Vector3 offset;


    void Start()
    {
        offset = transform.position - player.transform.position;
        overheadOn = true;

        overhead = Camera.main;
        playerCam = GetComponent<Camera>();
        playerCam.enabled = false;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        playerCam.transform.position = player.transform.position + offset;

        // bleh can't get rotiation working
        /*
        Quaternion currentRotation = playerCam.transform.rotation;
        currentRotation.y = player.transform.rotation.y;
        playerCam.transform.rotation = currentRotation;
        */
    }

    private void Update()
    {
        // switch camera on button toggle
        if (Input.GetKeyDown(KeyCode.C))
        {
            // switch to player
            if(overheadOn)
            {
                overhead.enabled = false;
                playerCam.enabled = true;
                overheadOn = false;
            }

            // switch to overhead
            else
            {
                overhead.enabled = true;
                playerCam.enabled = false;
                overheadOn = true;
            }
        }
    }


}
