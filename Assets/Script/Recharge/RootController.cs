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
    [Range(1,10)] public float m_RefillTick;

    private bool isKeyPressed;

    public void Start()
    {
        m_Dirted = false;
        root = GetComponent<Rootbar>();
        m_Time = 0;
        m_RefillTime = 0;
        m_Pc = GetComponent<PlayerController>();
        isKeyPressed = false;
    }

    public void Update()
    {
        m_Time += Time.deltaTime;
        m_RefillTime += Time.deltaTime;

        if (Input.GetKey(KeyCode.E) && isKeyPressed == false && m_Dirted)
        {
            isKeyPressed = true;
            m_Time = 0;
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
        
        if (m_Time > (RefillCountdown) / 1000)
        {
            m_Pc.canMove = true;
            isKeyPressed = false;
        }

    }

    public void RefillBar()
    {
        if(root.RootbarValue < root.RootbarMaxValue)
            root.RootbarValue += root.RootbarMaxValue / (m_RefillTick - 1);
    }

    private void OnCollisionEnter2D(Collision2D collision)  //This check if this game Object is colliding with something
    {
        if (collision.gameObject.CompareTag("Dirt"))    //We are checking if its colliding an Object with the Tag: Dirt
            m_Dirted = true; //We are touching the ground so its true
    }
    private void OnCollisionExit2D(Collision2D collision)  //This check if this game Object is colliding with something
    {
        if (collision.gameObject.CompareTag("Dirt"))    //We are checking if its colliding an Object with the Tag: Dirt
            m_Dirted = true; //We are touching the ground so its true
    }
}
