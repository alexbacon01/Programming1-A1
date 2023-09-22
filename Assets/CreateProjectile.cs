using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CreateProjectile : MonoBehaviour
{
    // Start is called before the first frame update
    public Rigidbody2D rb;
    public float forceAmount = 5f;
    public GameObject projectile;
    public GameObject target;
    Vector3 targetPos;
    public float currentTime = 2f;
    public bool isTimerRunning = false;
    public float coolDown = 4;
    public float abilityRadius = 3f;
    public Vector3 rotation;
    public float destroyTime = .8f;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        targetPos = target.GetComponent<Transform>().position;
        Boolean inRadius = CheckInRadius(abilityRadius, transform.position, targetPos);
        Debug.Log(inRadius);

     
    
        CoolDownTimer();
        if (currentTime >= coolDown && inRadius)
        {
            GameObject shotProjectile =  Create();
            Destroy(shotProjectile, destroyTime);
            currentTime = 0;


        }
  
        if (inRadius)
        {
            spriteRenderer.color = Color.red;
        }
        else
        {
            spriteRenderer.color = Color.yellow;
        }

       
    }


    GameObject Create()
    {
        isTimerRunning = true;
        return Instantiate(projectile, transform.position, AimProjectile());
    }

    void CoolDownTimer()
    {
        if (isTimerRunning)
        {
            currentTime += Time.deltaTime;
        }
    }
    Boolean CheckInRadius(float radius, Vector3 posA, Vector3 posB)
    {
 
        if (Vector3.Distance(posA, posB) <= radius)
        {
            Debug.Log("In ability radius!");

            return true;
        }
        return false;

    }
    Quaternion AimProjectile()
    {
        Vector2 vectorBetween = (targetPos - transform.position).normalized; //target - player to find the vector between two spots and normalize it

        Vector2 up = transform.up; //short hand for (0,1) direction
        Debug.DrawLine(transform.position, transform.position + (Vector3)up * 5f);
        Debug.DrawRay(transform.position, (Vector3)up * 5f);
        float signedAngle = Vector2.SignedAngle(up, vectorBetween); //angle between forward and the player
        Quaternion rotation = Quaternion.Euler(0, 0, signedAngle); //adds the roation but has to be done in a Vector3 to add the rotation angle
        return rotation;

        Debug.Log("rotating!");
    }


}
