using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VineController : MonoBehaviour
{
    private float m_Distance;
    public Vector2 m_SelectedPoint;
    private PlayerController m_Player;
    public GameObject tileVineStart;
    public GameObject tileVineLoop;
    public GameObject tileVineEnd;
    public GameObject ParentVine;
    public float CompassOffSetX = 0f;
    //"Radius" of the Vine Loop Tileset
    public float VineDistance = 0f; 
    //effective distance at witch the Vine Loop and Vine End Tileset should be placed (Vine Distance + CompassOffset, then VineOffSet += Vine Distance)
    private float m_VineOffSetX; 
    //Counter of how many time the Vine Loop Tileset have to be placed (do while)
    private float m_VineLoopAmount; 

    public GameObject m_MouseCompass;
    public float test;
    private float RotationAdjustement;

    private void Start()
    {
        //Calculating initial VineOffSet
        m_VineOffSetX = VineDistance + CompassOffSetX;
        m_Player = GetComponent<PlayerController>();
    }
    
    public void CalculateDistance(Vector2 MousePosition)
    {
        m_SelectedPoint = MousePosition;

        m_Distance = Vector2.Distance(transform.position, m_SelectedPoint);
    }

    public void CreateVine(Vector2 Direction)
    {
        GameObject tmp;
        GameObject JointManager;
        print(m_MouseCompass.transform.rotation.x);

        float position = m_Distance / 2;

        if (m_MouseCompass.transform.rotation.y < 0)
        {
            tmp = Instantiate(tileVineLoop,
                         new Vector2(m_MouseCompass.transform.position.x + (m_MouseCompass.transform.forward.x * position),
                                     m_MouseCompass.transform.position.y + (m_MouseCompass.transform.forward.y * position)),
                         Quaternion.Euler(0, 0, m_MouseCompass.transform.rotation.x * 180)) as GameObject;
        }
        else
        {
            tmp = Instantiate(tileVineLoop,
                         new Vector2(m_MouseCompass.transform.position.x + (m_MouseCompass.transform.forward.x * position),
                                     m_MouseCompass.transform.position.y + (m_MouseCompass.transform.forward.y * position)),
                         Quaternion.Euler(0, 0, -m_MouseCompass.transform.rotation.x * 180)) as GameObject;
        }
            
        tmp.gameObject.GetComponent<SpriteRenderer>().size = new Vector2(m_Distance, 1.57f);

        m_Player.Vine = tmp;
        //m_Player.Vine.gameObject.

        m_VineOffSetX = VineDistance + CompassOffSetX;
    }
}
