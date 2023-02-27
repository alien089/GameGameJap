using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Respawn : MonoBehaviour
{
    private Transform respawnPoint;
    
    private bool isDead;


    private void Start()
    {
        isDead = false;
        respawnPoint = GameObject.FindGameObjectWithTag("Respawn").transform;
    }

    public void Update()
    {
        if (isDead)
        {
            respawnToCheckpoint();
        }
        if (Input.GetKey(KeyCode.X))
            Death();
    }

    public void Death()
    {
        isDead = true;
    }

	private void OnTriggerEnter(Collider other)
	{
		if (other.TryGetComponent(out EnemySpyke ESpyke)) respawnToCheckpoint();
	}

	public void respawnToCheckpoint()
    {
        transform.position = new Vector2(respawnPoint.position.x, respawnPoint.position.y);
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        isDead = false;
    }

    public void CheckpointSet()
    {
        respawnPoint.transform.position = new Vector2(transform.position.x, transform.position.y);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Checkpoint")
            CheckpointSet();
        if (collision.gameObject.CompareTag("Death"))
        {
            Death();
        }
    }
}
