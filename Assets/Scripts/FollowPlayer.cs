using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    //Jaakon simppeli kamera seuraa pelaajaa-scripti

    public GameObject player;
    private Vector3 offset = new Vector3(0, 4, -8);


    void LateUpdate()
    {
        // Offset for the camera by adding to the player
        transform.position = player.transform.position + offset;

    }
}
