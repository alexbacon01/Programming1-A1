using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class npcMovement : MonoBehaviour
{

    public GameObject playerObject;
    public GameObject npc;
    //make vectors for the player, NPC and the vector between the two
    public Vector2 player;
    public Vector2 npcVector;
    public Vector2 vectorBetween;

    public float moveSpeed = 1f;

    public Boolean characterInRadius = false;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        vectorBetween = (player - npcVector).normalized; //target - player to find the vector between two spots and normalize it
        characterInRadius = checkInRadius();
        if (characterInRadius)
        {
            MoveTowardsPlayer(); //move towards player
            RotateTowardsPlayer(); //rotate the object 
        } else
        {
            Wander();
        }
    }

    void RotateTowardsPlayer()
    {
        Vector2 up = transform.up; //short hand for (0,1) direction

        Debug.DrawLine(transform.position, transform.position + (Vector3)up * 5f);

        Debug.DrawRay(transform.position, (Vector3)up * 5f);

        float signedAngle = Vector2.SignedAngle(up, vectorBetween); //angle between forward and the player

        transform.Rotate(new Vector3(0, 0, signedAngle)); //adds the roation but has to be done in a Vector3 to add the rotation angle
    }

    void MoveTowardsPlayer()
    {
        player = (playerObject.transform.position); //vector for player
        npcVector = (npc.transform.position); //vector for npc


        npc.transform.position = Vector2.MoveTowards(npc.transform.position, playerObject.transform.position, moveSpeed * Time.deltaTime); //move the npc towards the player
    }

    void Wander()
    {


    }

    Boolean checkInRadius()
    {

        float radius = 2f;

        if (Vector3.Distance(transform.position, playerObject.transform.position) < radius)
        {
            Debug.Log("In radius!");
            return true;
        }
       
        return false;

    }
}
