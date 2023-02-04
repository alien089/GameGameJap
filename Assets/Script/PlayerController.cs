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
    private Animator m_Animator;
    private SpriteRenderer m_SR;

    public bool m_Grounded; //To see if the player is on the ground
    private float m_HorizontalInput;

    public bool canMove;

    private Camera cam;
    Vector2 mousePos;

    private void Awake()
    {
        //References for rigidbody and m_Animator from object
        m_Body = GetComponent<Rigidbody2D>();
        m_Grounded = true;
        canMove = true;
    }
    void Start()
    {
        cam = Camera.main;
        mousePos = new Vector2();
        m_Animator = GetComponent<Animator>();
        m_SR = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (m_Grounded) m_Animator.SetBool("IsJumping", false);
        if (canMove)
        {
            m_HorizontalInput = Input.GetAxis("Horizontal");    //When you press leftKeys -1, rightKeys +1
            m_Body.velocity = new Vector2(m_HorizontalInput * Speed, m_Body.velocity.y);
            if(m_Body.velocity.magnitude > 0) m_Animator.SetBool("IsWalking", true);
            else m_Animator.SetBool("IsWalking", false);  //Movement of the pg

            if (Input.GetKey(KeyCode.LeftShift)) m_Animator.SetBool("IsRunning", true);
            else m_Animator.SetBool("IsRunning", false);

            if(m_Body.velocity.x < 0) m_SR.flipX = true;
            else if (m_Body.velocity.x > 0) m_SR.flipX = false;

            if (Input.GetKey(KeyCode.Space) && m_Grounded)    //If Space Key is pressed and the pg is not on the ground the pg is going to jump
                Jump(); //Calling Jump method
        }
        else
            m_Body.velocity = new Vector2(0, 0);
    }

    private void Jump()
    {
        m_Body.velocity = new Vector2(m_Body.velocity.x, JumpHeight);
        m_Animator.SetBool("IsJumping", true); //Move the pg above / jump
        m_Grounded = false; //We are not touching the ground so its false
    }

    private void OnCollisionEnter2D(Collision2D collision)  //This check if this game Object is colliding with something
    {
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Dirt"))    //We are checking if its colliding an Object with the Tag: Ground
            m_Grounded = true; //We are touching the ground so its true
    }
    /*
    private void OnCollisionExit2D(Collision2D collision)  //This check if this game Object is colliding with something
    {
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Dirt"))    //We are checking if its colliding an Object with the Tag: Ground
            m_Grounded = false; //We are touching the ground so its true
    }*/
    void OnGUI()
    {
        Event currentEvent = Event.current;
        mousePos.x = currentEvent.mousePosition.x;
        mousePos.y = cam.pixelHeight - currentEvent.mousePosition.y;
    }
}
