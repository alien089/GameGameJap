using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] public Image RootbarCurrent;
    private Rootbar root;

    public void Start()
    {
        root = GameObject.FindGameObjectWithTag("Player").GetComponent<Rootbar>();
        RootbarCurrent.fillAmount = root.RootbarValue / 100;
    }

    public void Update()
    {
        RootbarCurrent.fillAmount = root.RootbarValue / 100;
    }
}
