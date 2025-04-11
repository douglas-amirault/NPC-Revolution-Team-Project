using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YouWin : MonoBehaviour
{
    // this is simply so the Cursor shows up on the win menu screen in the build
    void Start()
    {
        Cursor.lockState = CursorLockMode.None; // Unlock cursor for UI
        Cursor.visible = true; // Make cursor visible
    }
}
