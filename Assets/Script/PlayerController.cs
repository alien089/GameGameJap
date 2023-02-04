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
    public bool canMove;
    private Camera m_Cam;

    public Vector2 MousePos;
    public Vector2 Direction;
    public StageManager StageManager;
    public GameObject Vine;

    public GameObject m_MouseCompass;
    private VineController m_Vine;

    private void Awake()
    {
        //References for rigidbody and m_Animator from object
        m_Body = GetComponent<Rigidbody2D>();
        m_Vine = GetComponent<VineController>();
        m_Grounded = true;
        canMove = true;
    }

    void Start()
    {
        m_Cam = Camera.main;
        MousePos = new Vector2();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        MousePos = StageManager.ConvertToWorldUnits(MousePos);
        float directionX = MousePos.x - transform.position.x;
        float directionY = MousePos.y - transform.position.y;

        Direction = new Vector2(directionX, directionY);
        
        if(canMove)
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

        m_MouseCompass.transform.forward = Direction;

        //Using of swing vine
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if(Vine == null)
            {
                //calculate distanza from player
                float distance = Vector2.Distance(MousePos, transform.position);

                RaycastHit2D hit = Physics2D.Linecast(MousePos, transform.position);
                Debug.DrawLine(transform.position, Direction, Color.red, 10.0f);

                if (hit.collider.gameObject.CompareTag("Vineable"))
                {
                    m_Vine.CalculateDistance(hit.collider.transform.position);
                    m_Vine.CreateVine(Direction);
                }
            }
            else
            {
                Destroy(Vine);
            }
        }
    }

    private void Jump()
    {
        m_Body.velocity = new Vector2(m_Body.velocity.x, JumpHeight); //Move the pg above / jump
        //m_Grounded = false; //We are not touching the ground so its false
    }

    void OnGUI()
    {
        Event currentEvent = Event.current;
        MousePos.x = currentEvent.mousePosition.x;
        MousePos.y = m_Cam.pixelHeight - currentEvent.mousePosition.y;
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
