using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RootController : MonoBehaviour
{
    [Range(1, 1000)] public float RefillCountdown;
    private PlayerController m_Pc;
    public bool m_Dirted;
    private Rootbar root;
    private float m_Time;
    private float m_RefillTime;
    [Range(1,100)] public float m_RefillTick;

    private Transform respawnPoint;
    private Respawn m_Respawn;
    private bool isCheckpointChecked;
    private AudioManager audio;

    private bool isKeyPressed;

    public void Start()
    {
        m_Dirted = false;
        root = GetComponent<Rootbar>();
        m_Time = 0;
        m_RefillTime = 0;
        m_Pc = GetComponent<PlayerController>();
        isKeyPressed = false;
        isCheckpointChecked = false;
        m_Respawn = GetComponent<Respawn>();
        respawnPoint = GameObject.FindGameObjectWithTag("Respawn").transform;
        audio = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
    }

    public void Update()
    {
        m_Time += Time.deltaTime;
        m_RefillTime += Time.deltaTime;

        if (Input.GetKey(KeyCode.E) && isKeyPressed == false && m_Dirted)
        {
            isKeyPressed = true;
            m_Time = 0;
            isCheckpointChecked = true;
            m_Pc.m_Body.velocity = new Vector2(0, 0);
            audio.PlaySFX(10);
        }

        if (isKeyPressed && m_Dirted && m_Time < (RefillCountdown / 1000))
        {
            m_Pc.canMove = false;
            if (m_RefillTime > ((RefillCountdown / m_RefillTick) / 1000))
            {
                RefillBar();
                m_RefillTime = 0;
            }
        }
        
        if (isKeyPressed && m_Time >= (RefillCountdown) / 1000)
        {
            m_Pc.canMove = true;
            isKeyPressed = false;
        }

        if (isCheckpointChecked)
            moveRespawnPoint();
    }

    public void RefillBar()
    {
        if(root.RootbarValue < root.RootbarMaxValue)
            root.RootbarValue += root.RootbarMaxValue / (m_RefillTick - 1);
    }

    public void moveRespawnPoint()
    {
        m_Respawn.CheckpointSet();
        isCheckpointChecked = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)  //This check if this game Object is colliding with something
    {
        if (collision.gameObject.CompareTag("Dirt"))    //We are checking if its colliding an Object with the Tag: Dirt
            m_Dirted = true; //We are touching the ground so its true
    }
    private void OnCollisionExit2D(Collision2D collision)  //This check if this game Object is colliding with something
    {
        if (collision.gameObject.CompareTag("Dirt"))    //We are checking if its colliding an Object with the Tag: Dirt
            m_Dirted = false; //We are touching the ground so its true
    }
}
