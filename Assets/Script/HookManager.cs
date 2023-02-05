using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HookManager : MonoBehaviour
{
    public GameObject RopeStart;
    public GameObject NodeTrasfer;
    GameObject curHook;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            print("AAAA");
            Vector2 Destiny = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            print("CACCA");
            curHook = (GameObject) Instantiate (RopeStart, Destiny, Quaternion.identity);
            print("CUCUCU");
            NodeTrasfer.GetComponent<RopeScript>().Destiny = Destiny;
        }
    }
}
