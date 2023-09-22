using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcAbilities : MonoBehaviour
{
    // Start is called before the first frame update
    public Rigidbody2D rb;
    public float forceAmount = 5f;
    public GameObject projectile;
    public float currentTime = 2f;
    public bool isTimerRunning = false;
    public float coolDown = 2;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        CoolDownTimer();
        if (Input.GetButtonDown("Jump") && currentTime >= coolDown)
        {

            CreateProjectile();
            currentTime = 0;
        }

        Debug.Log(currentTime);
    }


    void CreateProjectile()
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


}
