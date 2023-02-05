using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    private Camera m_MainCam;
    private Vector3 m_MousePos;
    public GameObject Bullet;
    public Transform BulletTransform;
    public bool CanShoot;
    private float m_timer;
    public float TimeBetweenShooting;

    // Start is called before the first frame update
    void Start()
    {
        m_MainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        m_MousePos = m_MainCam.ScreenToWorldPoint(Input.mousePosition);
        Vector3 rotation = m_MousePos - transform.position;
        float rotZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rotZ);
        if (!CanShoot)
        {
            m_timer += Time.deltaTime;
            if(m_timer > TimeBetweenShooting)
            {
                CanShoot = true;
                m_timer = 0;
            }
        }
        if(Input.GetMouseButtonDown(1) && CanShoot) 
        {
            CanShoot = false;
            Instantiate(Bullet, BulletTransform.position, Quaternion.identity);
        }
    }
}
