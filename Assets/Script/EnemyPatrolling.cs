using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrolling : MonoBehaviour
{
    [Header("Patrol Points")]
    [SerializeField] private Transform m_LeftEdge;
    [SerializeField] private Transform m_RightEdge;

    [Header("Enemy")]
    [SerializeField] private Transform m_Enemy;

    [Header("Movement parameters")]
    [SerializeField] private float m_Speed;
    private Vector3 m_InitScale;
    private bool m_MovingLeft;

    [SerializeField] private float m_IdleDuration;
    private float m_IdleTimer;
    //[SerializeField] private Animator m_Anim;

    private void Awake()
    {
        m_InitScale = m_Enemy.localScale;
        m_MovingLeft = true;
    }
    /*
    private void OnDisable()
    {
        m_Anim.SetBool("moving", false);
    }*/

    private void Update()
    {
        if (m_MovingLeft)
            if (m_Enemy.position.x >= m_LeftEdge.position.x)
                MoveInDirection(-1);
            else
            {
                DirectionChange();
            }
        else if (m_Enemy.position.x <= m_RightEdge.position.x)
            MoveInDirection(1);
        else
        {
            DirectionChange();
        }
    }

    private void DirectionChange()
    {
        //m_Anim.SetBool("moving", false);

        m_IdleTimer += Time.deltaTime;

        if (m_IdleTimer > m_IdleDuration)
            m_MovingLeft = !m_MovingLeft;
    }

    private void MoveInDirection(int Direction)
    {
        m_IdleTimer = 0;
        //m_Anim.SetBool("moving", true);

        m_Enemy.localScale = new Vector3(Mathf.Abs(m_InitScale.x) * Direction, m_InitScale.y, m_InitScale.z);

        m_Enemy.position = new Vector3(m_Enemy.position.x + Time.deltaTime * Direction * m_Speed,
            m_Enemy.position.y, m_Enemy.position.z);
    }
}
