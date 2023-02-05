using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VinesSystem : MonoBehaviour
{
    public Camera mainCamera;
    public GameObject Vine;
    private LineRenderer m_LineRenderer;
    private DistanceJoint2D m_DistanceJoint;
    private PlayerController m_Pc;

    public int SwingVelocity;
    public int RunningSwingVelocity;

    private RaycastHit hit;
    private Ray ray;

    private bool vineable;

    public void Start()
    {
        m_LineRenderer = GetComponent<LineRenderer>();
        m_DistanceJoint = GetComponent<DistanceJoint2D>();
        m_Pc = GetComponent<PlayerController>();
        m_DistanceJoint.enabled = false;
        m_LineRenderer.enabled = false;
        vineable = true;
    }

    public void Update()
    {

        ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        //if (Physics.Raycast(ray, out hit))
        //{
        //    Transform objectHit = hit.transform;
        //    if (objectHit.transform.tag == "Vineable")
        //        vineable = true;
        //    else
        //        vineable = false;
        //}
        //else
        //    vineable = false;

        if (Input.GetKeyDown(KeyCode.Mouse0) && vineable)
        {
            m_Pc.m_Grounded = true;
            m_Pc.canMove = false;
            Vector2 mousePos = (Vector2) mainCamera.ScreenToWorldPoint(Input.mousePosition);
            if(mousePos.x > m_Pc.transform.position.x && Input.GetKey(KeyCode.LeftShift))   //Swing while running
                m_Pc.m_Body.velocity = new Vector2(RunningSwingVelocity, 0);
            else if (mousePos.x > m_Pc.transform.position.x)
                    m_Pc.m_Body.velocity = new Vector2(SwingVelocity, 0);
            else if (mousePos.x <= m_Pc.transform.position.x && Input.GetKey(KeyCode.LeftShift))
                m_Pc.m_Body.velocity = new Vector2(-1 * RunningSwingVelocity, 0);
            else if (mousePos.x <= m_Pc.transform.position.x && Input.GetKey(KeyCode.LeftShift))
                m_Pc.m_Body.velocity = new Vector2(-1 * SwingVelocity, 0);

            m_LineRenderer.SetPosition(0, mousePos);
            Vector2 middlePoint = new Vector2((mousePos.x + transform.position.x)/2, (mousePos.y + transform.position.y)/2);
            Vector2 direction = new Vector2(transform.position.x - middlePoint.x, transform.position.y - middlePoint.y).normalized;
            Instantiate(Vine, middlePoint, Quaternion.LookRotation(direction));
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
        else if(m_DistanceJoint.enabled && vineable)
        {
            m_LineRenderer.SetPosition(1, transform.position);
        }
    }
}
