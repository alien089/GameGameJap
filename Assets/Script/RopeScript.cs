using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeScript : MonoBehaviour
{
    public Vector2 Destiny;

    public float Distance = 2f;

    public GameObject NodePrefab;

    public GameObject Player;

    public GameObject LastNode;

    public float speed = 1f;
    
    bool done = false;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");

        LastNode = transform.gameObject;
    } 

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, Destiny, speed);

        if ((Vector2)transform.position != Destiny)
        {
            if(Vector2.Distance(Player.transform.position, LastNode.transform.position) > Distance)
            {
                CreateNode ();
            }
        }
        else if(done == false)
        {
            done = true;

            LastNode.GetComponent<HingeJoint2D>().connectedBody = Player.GetComponent<Rigidbody2D>();
        }
    }

    void CreateNode()
    {
        print("AAAAAAAAA");
        Vector2 PositionToCreate = Player.transform.position - LastNode.transform.position;
        PositionToCreate.Normalize();
        PositionToCreate *= Distance;
        PositionToCreate += (Vector2)LastNode.transform.position;

        GameObject go = (GameObject)     Instantiate(NodePrefab, PositionToCreate, Quaternion.identity);

        go.transform.SetParent(transform);
        LastNode.GetComponent<HingeJoint2D>().connectedBody = go.GetComponent<Rigidbody2D>();

        LastNode = go;
    }
}
