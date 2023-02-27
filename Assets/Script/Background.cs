using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class Background : MonoBehaviour
{
	private Camera MainCamera;
	public GameObject Wall;
	public GameObject Glass;
	public GameObject Tree;

	public GameObject LeftGlass;
	public GameObject RightGlass;

	private int Repeat = 4;

	void Start()
	{
		MainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
	}
	void Update()
	{
		MoveTree();
		MoveGlass();
		MoveWall();
	}

	private void MoveTree()
	{
		if (MainCamera.transform.position.x > LeftGlass.transform.position.x && MainCamera.transform.position.x < RightGlass.transform.position.x)
			Tree.transform.position = new Vector3(MainCamera.transform.position.x, MainCamera.transform.position.y + 0.5f, MainCamera.transform.position.z + 6.5f);
		else 
			Tree.transform.position = new Vector3(Tree.transform.position.x, MainCamera.transform.position.y + 0.5f, MainCamera.transform.position.z + 6.5f);
	}

	private void MoveGlass()
	{ 
		if (MainCamera.transform.position.x > LeftGlass.transform.position.x + 5 && MainCamera.transform.position.x < RightGlass.transform.position.x - 5)
			Glass.transform.position = new Vector3(MainCamera.transform.position.x, MainCamera.transform.position.y, MainCamera.transform.position.z + 6.5f);
		else
			Glass.transform.position = new Vector3(Glass.transform.position.x, MainCamera.transform.position.y, MainCamera.transform.position.z + 6.5f);
	}

	private void MoveWall()
	{
		Wall.transform.position = new Vector3(MainCamera.transform.position.x, MainCamera.transform.position.y, MainCamera.transform.position.z + 6.5f);
	}
}
