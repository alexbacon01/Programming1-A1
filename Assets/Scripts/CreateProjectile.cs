using System;
using UnityEngine;

public class CreateProjectile : MonoBehaviour
{
    //refernces to the game objects for target and projectile
    public GameObject projectile;
    public GameObject target;

    public float currentTime = 0f; //float for current time
    public bool isTimerRunning = false; //publically accessible boolean for if timer is running
    public float coolDown = 3f; //the cooldown time amount
    public float abilityRadius = 3f; //the radius for abilities
    public float destroyTime = .8f; //public so that it can be changed from unity

    void Start()
    {
        currentTime = coolDown; //set current time to start at cooldown so that projectile fires immedia
    }

    // Update is called once per frame
    void Update()
    {
        //set the local variables
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>(); //get spriteRenderer to change colour while player is within radius
        Vector3 targetPos = target.GetComponent<Transform>().position; //get the target pos as a Vector3
        Boolean inRadius = CheckInRadius(abilityRadius, transform.position, targetPos); //boolean for if the object is within radius

        Debug.Log(inRadius); //is it in the radius??

        CoolDownTimer(); //start cool down timer

        if (currentTime >= coolDown && inRadius) //if the current time is less than cooldown and it's within radius
        {
            GameObject shotProjectile = Create(targetPos); //create game object
            Destroy(shotProjectile, destroyTime); //destroy object after destroyTime amount of time
            currentTime = 0; //set current time back to 0
        }

        if (inRadius) //if it is within radius 
        {
            spriteRenderer.color = Color.red; //set colour to red
        }
        else //if not within radius
        {
            spriteRenderer.color = Color.yellow; //set colour to yellow
        }
    }

    //function to create object, returns the new game object, and takes a Vector3 paramater for the target position
    GameObject Create(Vector3 targetPos)
    {
        isTimerRunning = true; //set timer running to true
        return Instantiate(projectile, transform.position, AimProjectile(targetPos)); //create and return the new game object 
    }

    //function for the cool down timer
    void CoolDownTimer()
    {
        if (isTimerRunning) //if the timer is running
        {
            currentTime += Time.deltaTime; //add Time.deltaTime to currentTime
        }
    }

    //a function to check if target is within radius of another object, returns a boolean value. takes parameters for the two objects to check distance between
    Boolean CheckInRadius(float radius, Vector3 posA, Vector3 posB)
    {
        if (Vector3.Distance(posA, posB) <= radius) //if the distance between pos a and b is less than radius
        {
            Debug.Log("In ability radius!"); //its in the radius!!
            return true; //return true
        }
        return false; //if not return false
    }

    //a function to find the rotation the projectile should be shot from. Returns a Quaternion and takes a Vector3 as a parameter of the target position
    Quaternion AimProjectile(Vector3 targetPos)
    {
        Vector2 vectorBetween = (targetPos - transform.position).normalized; //target - player to find the vector between two spots and normalize it
        Vector2 up = transform.up; //short hand for (0,1) direction

        float signedAngle = Vector2.SignedAngle(up, vectorBetween); //angle between forward and the player
        Quaternion rotation = Quaternion.Euler(0, 0, signedAngle); //adds the roation but has to be done in a Vector3 to add the rotation angle
        return rotation;

    }
}
