using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI : MonoBehaviour
{
	public GameObject Input;
	private void OnTriggerStay2D(Collider2D other)
	{
		if (other.CompareTag("Player"))
		{
			Input.SetActive(true);
		}
		else
		{
			Input.SetActive(false);
		} 
	}
}
