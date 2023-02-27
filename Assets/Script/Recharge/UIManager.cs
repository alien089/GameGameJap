using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Image RootbarCurrent1;
	public Image RootbarCurrent2;
	public Image RootbarCurrent3;
	private Rootbar root;

    public void Start()
    {
        root = GameObject.FindGameObjectWithTag("Player").GetComponent<Rootbar>();
	}

    public void Update()
    {
        RootbarCurrent1.fillAmount = root.RootbarValue / 100;
		RootbarCurrent2.fillAmount = root.RootbarValue / 100;
		RootbarCurrent3.fillAmount = root.RootbarValue / 100;
	}
}
