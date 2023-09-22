using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Windows;
using Random = UnityEngine.Random; //use unity random

public class npcMovement : MonoBehaviour
{

    public GameObject playerObject;
    public GameObject npc;
    //make vectors for the player, NPC and the vector between the two
    public Vector2 player;
    public Vector2 npcVector;
    public Vector2 vectorBetween;

    public Vector2 originPos; //position the NPC starts at

    public float moveSpeed = 1f;
    public float npcAttackRadius = 3f;
    public float wanderRadius = 2f;

    public Vector2 newPos;


    public Boolean characterInRadius = false;
    public Boolean npcMoving = false;
    public Boolean foundPlayer = false;


    // Start is called before the first frame update
    void Start()
    {
        originPos = transform.position;
        newPos = transform.position;
        
    }

    // Update is called once per frame
    void Update()
    {

        player = playerObject.transform.position;
        npcVector = npc.transform.position;

        vectorBetween = (player - npcVector).normalized; //target - player to find the vector between two spots and normalize it
        characterInRadius = CheckInRadius(npcAttackRadius, transform.position, playerObject.transform.position); //check to see if character is in radius of the NPC
 
        if (!npcMoving)
        {
            newPos = getNewPos();
            npcMoving = true;
        } else if (characterInRadius) //if the character is within the NPC's attack radius 
        {
            
            MoveTowardsTarget(transform.position, playerObject.transform.position); //move towards player
            RotateTowardsPlayer(); //rotate the object 
            foundPlayer = true;
        } else if(!characterInRadius && foundPlayer)
        {
            MoveTowardsTarget(transform.position, originPos);
            Debug.Log("character lsot");
            if (npcVector == originPos)
            {
                foundPlayer = false;
            }
        }
        else//if player is not within attack radius
        {

          MoveTowardsTarget(transform.position, newPos);
        }

        if(npcVector == originPos)
        {
            Debug.Log("at origin");
        }

        Debug.Log(characterInRadius);
    }
    void RotateTowardsPlayer()
    {
        
        Vector2 up = transform.up; //short hand for (0,1) direction
        Debug.DrawLine(transform.position, transform.position + (Vector3)up * 5f);
        Debug.DrawRay(transform.position, (Vector3)up * 5f);
        float signedAngle = Vector2.SignedAngle(up, vectorBetween); //angle between forward and the player
        transform.Rotate(new Vector3(0, 0, signedAngle)); //adds the roation but has to be done in a Vector3 to add the rotation angle

        Debug.Log("rotating!");
    }

    void MoveTowardsTarget(Vector3 start, Vector3 end)
    {
        transform.position = Vector3.MoveTowards(start, end, moveSpeed * Time.deltaTime); //move the npc to target
        if(transform.position == end)
        {
            Debug.Log("AT END");
            npcMoving = false;
        }
    }


    Vector2 getNewPos()
    {
        float minX = originPos.x + -wanderRadius;
        float maxX = originPos.x + wanderRadius;
        float minY = originPos.y + -wanderRadius;
        float maxY = originPos.y + wanderRadius;
        Vector2 newPos = transform.position;
 
        
            float xPos = Random.Range(minX, maxX);
            float yPos = Random.Range(minY, maxY);

            newPos = new Vector2(xPos, yPos);

            Debug.Log(maxX + " " + minX);
        

        return newPos;
    }

    Boolean CheckInRadius(float radius , Vector3 posA, Vector3 posB)
    {
 
        if (Vector3.Distance(posA, posB) <= radius)
        {
            Debug.Log("In chase radius!");
            
            return true;
        }
       
        return false;

    }
}
