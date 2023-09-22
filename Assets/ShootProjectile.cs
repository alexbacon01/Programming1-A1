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

    public SpriteRenderer sr;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = target.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        sr.color = Color.blue;

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
        sr.color = Color.red;
    }
}
