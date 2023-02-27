using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]

public class PlayerController : MonoBehaviour
{
    [Range(0, 100)] public float Speed; //Movement Speed of the pg
    [Range(0, 100)] public float RunningSpeed;
    [Range(0, 100)] public float JumpHeight; //How high is the jump 
    private AudioManager audio;

    public Rigidbody2D m_Body;

    private Animator m_Animator;
	private Animator m_SpawnAnimator;
    private SpriteRenderer m_SR;
	private InitialSpawn Respawn;
	private Rootbar m_Root;

    private bool m_Grounded; //To see if the player is on the ground
    private float m_HorizontalInput;

    public bool canMove;
    public bool isMoving;

    private Camera cam;
    Vector2 mousePos;

	private float RespawnCD = 2f;
	private float RespawnTimer;
    private void Awake()
    {
        GameObject.FindGameObjectWithTag("Respawn");
        //References for rigidbody and m_Animator from object
        m_Body = GetComponent<Rigidbody2D>();
        audio = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
        m_Grounded = true;
        canMove = true;
    }
    void Start()
    {
        cam = Camera.main;
        mousePos = new Vector2();
        m_Animator = GetComponent<Animator>();
        m_SR = GetComponent<SpriteRenderer>();
		m_Root = GetComponent<Rootbar>();
		Respawn = GameObject.FindGameObjectWithTag("Respawn").GetComponent<InitialSpawn>();
		m_SpawnAnimator = GameObject.FindGameObjectWithTag("Respawn").GetComponentInChildren<Animator>();
	}

    // Update is called once per frame
    void Update()
    {
		RespawnTimer += Time.deltaTime;
        if (m_Body.velocity.magnitude != 0f) isMoving = true;
        else isMoving = false;
        if (m_Grounded) m_Animator.SetBool("IsJumping", false);
        if (canMove)
        {
            m_HorizontalInput = Input.GetAxis("Horizontal");    //When you press leftKeys -1, rightKeys +1
            if (!Input.GetKey(KeyCode.LeftShift))
                m_Body.velocity = new Vector2(m_HorizontalInput * Speed, m_Body.velocity.y);
            else
                m_Body.velocity = new Vector2(m_HorizontalInput * RunningSpeed, m_Body.velocity.y);
            if (m_Body.velocity.magnitude > 0) m_Animator.SetBool("IsWalking", true);
            else m_Animator.SetBool("IsWalking", false);  //Movement of the pg

            if (Input.GetKey(KeyCode.LeftShift)) m_Animator.SetBool("IsRunning", true);
            else m_Animator.SetBool("IsRunning", false);

            if (m_Body.velocity.x < 0) m_SR.flipX = true;
            else if (m_Body.velocity.x > 0) m_SR.flipX = false;

            if (Input.GetKey(KeyCode.Space) && m_Grounded)
            {   //If Space Key is pressed and the pg is not on the ground the pg is going to jump
                Jump(); //Calling Jump method
                audio.PlaySFX(3);
            }
        }

		RespawnAnchor();
    }

    public void Jump()
    {
        m_Body.velocity = new Vector2(m_Body.velocity.x, JumpHeight);
        m_Animator.SetBool("IsJumping", true); //Move the pg above / jump
        m_Grounded = false; //We are not touching the ground so its false
    }
	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "NextLevel")
		{
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
			Destroy(Respawn.gameObject);
		}
		else if (other.tag == "End")
		{
			SceneManager.LoadScene("MenuScene");
			Destroy(Respawn.gameObject);
		}
	}

	private void OnCollisionEnter2D(Collision2D collision)  //This check if this game Object is colliding with something
    {
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Dirt") || collision.gameObject.CompareTag("Glass") || collision.gameObject.CompareTag("Stone") || collision.gameObject.CompareTag("Wood"))    //We are checking if its colliding an Object with the Tag: Ground
            m_Grounded = true; //We are touching the ground so its true
        if (collision.gameObject.CompareTag("Water"))
        {
            audio.PlaySFX(2);
        }
        if (collision.gameObject.CompareTag("Glass"))
        {
            audio.PlaySFX(4);
        }
        if (collision.gameObject.CompareTag("Dirt"))
        {
            audio.PlaySFX(5);
        }
        if (collision.gameObject.CompareTag("Wood"))
        {
            audio.PlaySFX(6);
        }
        if (collision.gameObject.CompareTag("Glass"))
        {
            audio.PlaySFX(7);
        }
    }

    /*
    private void OnCollisionExit2D(Collision2D collision)  //This check if this game Object is colliding with something
    {
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Dirt"))    //We are checking if its colliding an Object with the Tag: Ground
            m_Grounded = false; //We are touching the ground so its true
    }*/

	private void RespawnAnchor()
	{
		m_SpawnAnimator.SetBool("Reset", false);
		if (Input.GetKeyDown(KeyCode.E) && m_Grounded == true && RespawnTimer > RespawnCD)
		{
			m_Animator.SetBool("IsPlanting", true);
			canMove= false;
			RespawnTimer = 0;
			StartCoroutine(Wait());
		}
	}

	IEnumerator Wait()
	{
		yield return new WaitForSeconds(1.1f);
		m_Root.RootbarValue = 100;
		m_SpawnAnimator.SetBool("Reset", true);	
		Respawn.transform.position = new Vector3 (transform.position.x, transform.position.y - 0.8f, transform.position.z);
		m_Animator.SetBool("IsPlanting", false);
		canMove= true;
	}

    void OnGUI()
    {
        Event currentEvent = Event.current;
        mousePos.x = currentEvent.mousePosition.x;
        mousePos.y = cam.pixelHeight - currentEvent.mousePosition.y;
    }
}
