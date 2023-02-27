using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rootbar : MonoBehaviour
{

    [Range(0, 100)] public float RootbarValue;
    public float RootbarMaxValue;

    public void Start()
    {
        RootbarMaxValue = RootbarValue;
    }

    public void Update()
    {
        if(RootbarValue > RootbarMaxValue)
        {
            RootbarValue = RootbarMaxValue;
        }
    }
}
