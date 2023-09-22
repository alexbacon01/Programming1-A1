using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Start is called before the first frame update

    public Vector2 input; //Vector to hold input
    public float speed = 5f; //the force amount on movement
    public Vector2 normalizedInput; //variable to hold normalized input
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")); //get the input from the keyboard
        normalizedInput = input.normalized; //normalize the input
    }

    void FixedUpdate()
    {
        moveCharacter();
    }


    void moveCharacter()
    {
        transform.Translate(normalizedInput * speed * Time.deltaTime); 
    }



}
