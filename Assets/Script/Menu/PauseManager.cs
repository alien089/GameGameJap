using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    [SerializeField] public Canvas pauseCanvas;
    [SerializeField] public Canvas rootbarCanvas;
    [SerializeField] public Canvas optionsCanvas;
    private Respawn respawn;
    private bool isPaused;
    private AudioManager audio;

    public void Awake()
    {
        
    }

    public void Start()
    {
        pauseCanvas.gameObject.SetActive(false);
        optionsCanvas.gameObject.SetActive(false);
        isPaused = false;
        audio = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
//		rootbarCanvas = GameObject.FindGameObjectsWithTag("UI").GetComponent<GameObject>();
        respawn = GameObject.FindGameObjectWithTag("Player").GetComponent<Respawn>();
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Q))
            if(!isPaused)
            {
                PauseGame();
                isPaused = true;
            }
            else
            {
                ResumeGame();
                isPaused = false;
            }
    }

    public void PauseGame()
    {
        audio.PlaySFX(0);
        pauseCanvas.gameObject.SetActive(true);
        rootbarCanvas.gameObject.SetActive(false);
        Time.timeScale = 0f;
    }

    public void ResumeGame()
    {
        audio.PlaySFX(0);
        pauseCanvas.gameObject.SetActive(false);
        rootbarCanvas.gameObject.SetActive(true);
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void LastCheckpoin()
    {
        audio.PlaySFX(0);
        respawn.respawnToCheckpoint();
        pauseCanvas.gameObject.SetActive(false);
        rootbarCanvas.gameObject.SetActive(true);
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void ReturnMainMenu()
    {
        audio.PlaySFX(0);
        Time.timeScale = 1f;
        isPaused = false;
        SceneManager.LoadScene("MenuScene");
    }

    public void OptionsMenu()
    {
        audio.PlaySFX(0);
        pauseCanvas.gameObject.SetActive(false);
        optionsCanvas.gameObject.SetActive(true);
    }

    public void backToPauseMenu()
    {
        audio.PlaySFX(0);
        pauseCanvas.gameObject.SetActive(true);
        optionsCanvas.gameObject.SetActive(false);
    }
}
