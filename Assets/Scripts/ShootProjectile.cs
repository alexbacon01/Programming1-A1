using UnityEngine;

public class ShootProjectile : MonoBehaviour
{
    public Rigidbody2D rb; //the rigid body of projectile
    public float forceAmount = 1f; //force amount
    public GameObject target; //game object for target

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); //get componenet rigid body
    }

    // Update is called once per frame
    void Update()
    {
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
        collision.gameObject.GetComponent<SpriteRenderer>().color = Color.red; //when it collides with object change colour to red 
        Debug.Log(collision.gameObject.name); //what did it hit??
    }
}
