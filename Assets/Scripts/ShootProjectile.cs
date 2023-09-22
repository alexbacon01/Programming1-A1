using UnityEngine;

public class ShootProjectile : MonoBehaviour
{
    public Rigidbody2D rb; //the rigid body of projectile
    public float forceAmount = 1f; //force amount
    public GameObject target; //game object for target
    public SpriteRenderer sr; //sprite renderer 

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); //get componenet rigid body
        sr = target.GetComponent<SpriteRenderer>(); //get component sprite renderer 
    }

    // Update is called once per frame
    void Update()
    {
        sr.color = Color.blue; //set the target sprite to color blue
    }
    private void FixedUpdate()
    {
        if (rb != null) //if rb is not null
        {
            MoveProjectile(); //move the projectile
        }
    }

    //function to move the projectile
    void MoveProjectile()
    {
        rb.AddForce(transform.up * forceAmount, ForceMode2D.Impulse); //add force with impulse to shoot it instantly
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        sr.color = Color.red; //when it collides with object change colour to red NOT WORKING
        Debug.Log(collision.gameObject.name); //what did it hit??
    }
}
