using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Trailer : MonoBehaviour
{
	public float Duration = 0f;
	// Start is called before the first frame update
    void Start()
    {
		StartCoroutine(Wait());
    }

	private void Update()
	{
		if(Input.GetKeyDown(KeyCode.Space)) SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
	}

	IEnumerator Wait()
	{
		yield return new WaitForSeconds(Duration);
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
	}
}
