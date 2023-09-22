using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootProjectile : MonoBehaviour
{
    // Start is called before the first frame update
    public Rigidbody2D rb;
    public float forceAmount = 1f;
    public GameObject target;
    public Boolean targetHit = false;
    public SpriteRenderer spriteRenderer;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = target.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (targetHit)
        {
            Debug.Log("hit");
            spriteRenderer.color = Color.red;
        }

    }
    private void FixedUpdate()
    {
        if (rb != null)
        {
            MoveProjectile();
        }
    }

    void MoveProjectile()
    {
        rb.AddForce(transform.up * forceAmount, ForceMode2D.Impulse);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        targetHit = true;
            
        
        
    }
}
