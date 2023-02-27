using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgeController : MonoBehaviour
{
    private Vector3 m_MousePos;
    private Camera m_Camera;
    private Rigidbody2D rb;
    public float Force;
    public GameObject LeftBridge;
    public GameObject RightBridge;
    private PlayerController m_Player;
    private Rootbar root;
    public SpriteRenderer sr;
    private AudioManager audio;

    // Start is called before the first frame update
    void Start()
    {
        m_Camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        rb = GetComponent<Rigidbody2D>();
        m_MousePos = m_Camera.ScreenToWorldPoint(Input.mousePosition);
        Vector3 direction = m_MousePos - transform.position;
        Vector3 rotation = transform.position - m_MousePos;
        rb.velocity = new Vector2(direction.x, direction.y).normalized * Force;
        float rot = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0,0 , rot+ 90);
        m_Player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        root = GameObject.FindGameObjectWithTag("Player").GetComponent<Rootbar>();
        audio = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
        //sr = GetComponent<SpriteRenderer>();
    }

 /*   private void OnCollisionEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Dirt") 
            || collision.gameObject.CompareTag("Glass") || collision.gameObject.CompareTag("Stone") 
            || collision.gameObject.CompareTag("Wood") || collision.gameObject.CompareTag("Bridgeable"))
        {
            Destroy(this);
            if (collision.gameObject.CompareTag("Bridgeable"))
            {
                Instantiate(Bridge, transform.position, Quaternion.identity);
                if(transform.position.x - m_Player.transform.position.x < 0)
                {
                    sr.flipX = true;
                }
            }
        }
    }*/

    private void OnCollisionEnter2D(Collision2D collision)  //This check if this game Object is colliding with something
    {
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Dirt") 
            || collision.gameObject.CompareTag("Glass") || collision.gameObject.CompareTag("Stone") 
            || collision.gameObject.CompareTag("Wood") || collision.gameObject.CompareTag("Bridgeable"))
        {
            if (collision.gameObject.CompareTag("Bridgeable"))
            {
                if (root.RootbarValue >= 10)
                {
                    if (transform.position.x - m_Player.transform.position.x >= 0)
                    {
						Vector3 BridgeStart = new Vector3(transform.position.x - 2.8f, transform.position.y, transform.position.z);
                        Instantiate(LeftBridge, BridgeStart, Quaternion.identity);
                        root.RootbarValue -= root.RootbarMaxValue / 10f;
                        audio.PlaySFX(8);
                    }
                    else
                    {
						Vector3 BridgeStart = new Vector3(transform.position.x + 2.8f, transform.position.y, transform.position.z);
						Instantiate(RightBridge, BridgeStart, Quaternion.identity);
                        root.RootbarValue -= root.RootbarMaxValue / 10f;
                        audio.PlaySFX(8);
                    }
                }               
            }
            Destroy(gameObject);
        }
    }
}
