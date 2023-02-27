using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitialSpawn : MonoBehaviour
{
    public GameObject m_PrefabPlayer;
    private GameObject Respawn;
    private void Awake()
    {
        Respawn = GameObject.FindGameObjectWithTag("Respawn");
		DontDestroyOnLoad(Respawn);
        Spawn();
    }

    private void Spawn()
    {
        Instantiate(m_PrefabPlayer, Respawn.gameObject.transform.position, Quaternion.identity);
    }
}
