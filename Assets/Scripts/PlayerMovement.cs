using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f; //the force amount on movement

    // Start is called before the first frame update
    void Start()
    {
    }
    // Update is called once per frame
    void Update()
    {
        Vector2 normalizedInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")).normalized; //get the input from the keyboard and normalize the input
        MoveCharacter(normalizedInput);
    }

    //VERY simple function to move character
    void MoveCharacter(Vector2 input)
    {
        transform.Translate(input * speed * Time.deltaTime); //move the character in direction of input byt Time.deltaTime * speed
    }
}
