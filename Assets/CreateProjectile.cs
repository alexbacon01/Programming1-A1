using System;
using System.Collections;
using System.Collections.Generic;
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
    public float coolDown = 2;
    public float abilityRadius = 4f;

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

            Create();
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


    void Create()
    {
        Instantiate(projectile, transform.position, transform.rotation);
        isTimerRunning = true;
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

}
