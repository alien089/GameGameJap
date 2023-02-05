using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D.Path;
using UnityEngine;

public class VinesSystem : MonoBehaviour
{
    public Camera mainCamera;
    private LineRenderer m_LineRenderer;
    private DistanceJoint2D m_DistanceJoint;
    private PlayerController m_Pc;
    private Rootbar root;
    private AudioManager audio;

    public int SwingVelocity;
    public int RunningSwingVelocity;

    private RaycastHit2D hit;
    private Vector2 mousePos;

    private bool vineable;

    public void Start()
    {
        m_LineRenderer = GetComponent<LineRenderer>();
        m_DistanceJoint = GetComponent<DistanceJoint2D>();
        m_Pc = GetComponent<PlayerController>();
        root = GetComponent<Rootbar>();
        m_DistanceJoint.enabled = false;
        m_LineRenderer.enabled = false;
        vineable = false;
        audio = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
    }

    public void Update()
    {
        if(root.RootbarValue > 0)
        {
            if (Input.GetKey(KeyCode.W) && m_DistanceJoint.enabled == true)
            {
                m_DistanceJoint.distance -= 0.1f;
                root.RootbarValue -= 0.1f;
            }
            if (Input.GetKey(KeyCode.S) && m_DistanceJoint.enabled == true)
            {
                m_DistanceJoint.distance += 0.1f;
                root.RootbarValue -= 0.1f;
            }
        }
        

        mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        
        hit = Physics2D.Linecast(mousePos, transform.position);
        Debug.DrawLine(transform.position, mousePos, Color.red, 10.0f);
        if (hit.collider.CompareTag("Ground") || hit.collider.CompareTag("Bridgeable"))
        {
            vineable = true;
            mousePos = hit.collider.transform.position;
        }
        else
            vineable = false;

        if (Input.GetKeyDown(KeyCode.Mouse0) && vineable && root.RootbarValue > 0)
        {
            //m_Pc.m_Grounded = true;
            m_Pc.canMove = false;
            audio.PlaySFX(9);
            if (mousePos.x > m_Pc.transform.position.x && Input.GetKey(KeyCode.LeftShift))   //Swing while running
                m_Pc.m_Body.velocity = new Vector2(RunningSwingVelocity, 0);
            else if (mousePos.x > m_Pc.transform.position.x)
                m_Pc.m_Body.velocity = new Vector2(SwingVelocity, 0);
            else if (mousePos.x <= m_Pc.transform.position.x && Input.GetKey(KeyCode.LeftShift))
                m_Pc.m_Body.velocity = new Vector2(-1 * RunningSwingVelocity, 0);
            else if (mousePos.x <= m_Pc.transform.position.x && Input.GetKey(KeyCode.LeftShift))
                m_Pc.m_Body.velocity = new Vector2(-1 * SwingVelocity, 0);

            m_LineRenderer.SetPosition(0, mousePos);
            /*Vector2 middlePoint = new Vector2((mousePos.x + transform.position.x)/2, (mousePos.y + transform.position.y)/2);
            Vector2 direction = new Vector2(transform.position.x - middlePoint.x, transform.position.y - middlePoint.y).normalized;
            Instantiate(Vine, middlePoint, Quaternion.LookRotation(direction));*/
            //m_LineRenderer.SetPosition(0, transform.position);
            m_DistanceJoint.connectedAnchor = mousePos;
            m_DistanceJoint.enabled = true;

            m_LineRenderer.enabled = true;
        }
        else if (Input.GetKeyUp(KeyCode.Mouse0) && vineable)
        {
            m_DistanceJoint.enabled = false;
            m_LineRenderer.enabled = false;
            m_Pc.canMove = true;
        }
        else if (m_DistanceJoint.enabled && vineable)
        {
            m_LineRenderer.SetPosition(1, transform.position);
        }
        
       
    }
}
