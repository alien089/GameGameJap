using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]

public class PlayerController : MonoBehaviour
{
    [Range (0,100)] public float Speed; //Movement Speed of the pg
    [Range(0, 100)] public float JumpHeight; //How high is the jump 
    private Rigidbody2D m_Body;
    private bool m_Grounded; //To see if the player is on the ground
    private float m_HorizontalInput;

    private void Awake()
    {
        //References for rigidbody and m_Animator from object
        m_Body = GetComponent<Rigidbody2D>();
        m_Grounded = true;
    }

    // Update is called once per frame
    void Update()
    {
        m_HorizontalInput = Input.GetAxis("Horizontal");    //When you press leftKeys -1, rightKeys +1
        m_Body.velocity = new Vector2(m_HorizontalInput * Speed, m_Body.velocity.y);  //Movement of the pg

        if (m_HorizontalInput < 0.001f)    //if he is facing left turn/flip left
            transform.localScale = new Vector3(-1, 1, 1);
        else if (m_HorizontalInput > 0.001f)  //else turn/flip right
            transform.localScale = new Vector3(1, 1, 1);

        if (Input.GetKey(KeyCode.Space) && m_Grounded)    //If Space Key is pressed and the pg is not on the ground the pg is going to jump
            Jump(); //Calling Jump method
    }

    private void Jump()
    {
        m_Body.velocity = new Vector2(m_Body.velocity.x, JumpHeight); //Move the pg above / jump
        //m_Grounded = false; //We are not touching the ground so its false
    }

    private void OnCollisionEnter2D(Collision2D collision)  //This check if this game Object is colliding with something
    {
        if (collision.gameObject.CompareTag("Ground") && collision.gameObject.CompareTag("Dirt"))    //We are checking if its colliding an Object with the Tag: Ground
            m_Grounded = true; //We are touching the ground so its true
    }
    private void OnCollisionExit2D(Collision2D collision)  //This check if this game Object is colliding with something
    {
        if (collision.gameObject.CompareTag("Ground") && collision.gameObject.CompareTag("Dirt"))    //We are checking if its colliding an Object with the Tag: Ground
            m_Grounded = false; //We are touching the ground so its true
    }
}
