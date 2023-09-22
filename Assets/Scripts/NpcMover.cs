using System;
using UnityEngine;
using Random = UnityEngine.Random; //use unity random

public class NpcMover : MonoBehaviour
{
    public GameObject playerObject; //object for the player character

    //make vectors for the player, NPC and the vector between the two
    public Vector2 player;
    public Vector2 npcVector;
    public Vector2 vectorBetween;

    public Vector2 originPos; //position the NPC starts at

    public float moveSpeed = 1f; //speed the NPC moves at
    public float npcChaseRadius = 2f; //the radius at which the NPC chases a player
    public float wanderRadius = 2f; //the radius the NPC wanders around

    public Vector2 newPos; //the new positiom for the NPC to move to

    //booleans for checks in update
    public Boolean characterInRadius = false; 
    public Boolean npcMoving = false;
    public Boolean foundPlayer = false;


    // Start is called before the first frame update
    void Start()
    {
        originPos = transform.position; //set  the originPos to the original position of the NPC
    }

    // Update is called once per frame
    void Update()
    {
        player = playerObject.transform.position; //assign the player Objects position to vector2
        npcVector = transform.position; //assign NPCVector the position of transform 

        vectorBetween = (player - npcVector).normalized; //target - player to find the vector between two spots and normalize it
        characterInRadius = CheckInRadius(npcChaseRadius, transform.position, playerObject.transform.position); //check to see if character is in radius of the NPC

        if (!npcMoving) //check that the NPC is not already moving
        {
            newPos = GetNewPos(); //get a new position for npc to move to
            npcMoving = true; //set moving to true
        }
        else if (characterInRadius) //else if the character is within the NPC's attack radius 
        {
            MoveTowardsTarget(transform.position, playerObject.transform.position); //move towards player
            RotateTowardsPlayer(); //rotate the object towards player
            foundPlayer = true; //set found player to true
        }
        else if (!characterInRadius && foundPlayer) //else if the character is not in the radius but has been found 
        {
            MoveTowardsTarget(transform.position, originPos); //move npc back to the origin

            if (npcVector == originPos) //if the npc is back at origin 
            {
                foundPlayer = false; //set the found player back to false
            }
        }
        else//if player is not within attack radius
        {
            MoveTowardsTarget(transform.position, newPos); //move the npc randomly within it's radius
        }
    }
    
    //function to make the npc rotate to face the player object
    void RotateTowardsPlayer()
    {
        Vector2 up = transform.up; //short hand for (0,1) direction
        Debug.DrawLine(transform.position, transform.position + (Vector3)up * 5f); //debugging to see NPC is facing the right direction
        float signedAngle = Vector2.SignedAngle(up, vectorBetween); //angle between forward and the player
        transform.Rotate(new Vector3(0, 0, signedAngle)); //adds the rotaion but has to be done in a Vector3 to add the rotation angle
    }

    //function to move the NPC towards a target area, takes 2 paramaters a start and end position as Vector3s
    void MoveTowardsTarget(Vector3 start, Vector3 end)
    {
        transform.position = Vector3.MoveTowards(start, end, moveSpeed * Time.deltaTime); //move the npc to target

        if (transform.position == end) //if it has reached the target
        {
            Debug.Log("AT END"); 
            npcMoving = false; //set moving to false
        }
    }

    //a function to get a new random position, returns a Vector2
    Vector2 GetNewPos()
    {
        //local variables to get min and maxes for x and y values using the wanderRadius
        float minX = originPos.x + -wanderRadius;
        float maxX = originPos.x + wanderRadius;
        float minY = originPos.y + -wanderRadius;
        float maxY = originPos.y + wanderRadius;

        //use unity engines Random to get a float position within the min and max ranges 
        float xPos = Random.Range(minX, maxX);
        float yPos = Random.Range(minY, maxY);

        return new Vector2(xPos, yPos); //return the new random position
    }

    //a function to check if target is within radius of another object, returns a boolean value. takes parameters for the two objects to check distance between.
    Boolean CheckInRadius(float radius, Vector3 posA, Vector3 posB)
    {
        if (Vector3.Distance(posA, posB) <= radius) //if the distance between posA and posB is less or equal to the radius
        {
            Debug.Log("In radius!"); //it's in the radius!
            return true; //return true
        }

        return false; //otherwise return false
    }

    //function that returns a vector2 of the origin position, takes in a parameter for antoher vectro2

}
