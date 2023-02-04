using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    [SerializeField] public Canvas pauseCanvas;
    [SerializeField] public Canvas rootbarCanvas;
    [SerializeField] public Canvas optionsCanvas;
    [SerializeField] public Respawn respawn;
    private bool isPaused;

    public void Awake()
    {
        
    }

    public void Start()
    {
        pauseCanvas.gameObject.SetActive(false);
        optionsCanvas.gameObject.SetActive(false);
        isPaused = false;
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
        pauseCanvas.gameObject.SetActive(true);
        rootbarCanvas.gameObject.SetActive(false);
        Time.timeScale = 0f;
    }

    public void ResumeGame()
    {
        pauseCanvas.gameObject.SetActive(false);
        rootbarCanvas.gameObject.SetActive(true);
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void LastCheckpoin()
    {
        respawn.respawnToCheckpoint();
        pauseCanvas.gameObject.SetActive(false);
        rootbarCanvas.gameObject.SetActive(true);
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void ReturnMainMenu()
    {
        Time.timeScale = 1f;
        isPaused = false;
        SceneManager.LoadScene("MenuScene");
    }

    public void OptionsMenu()
    {
        pauseCanvas.gameObject.SetActive(false);
        optionsCanvas.gameObject.SetActive(true);
    }

    public void backToPauseMenu()
    {
        pauseCanvas.gameObject.SetActive(true);
        optionsCanvas.gameObject.SetActive(false);
    }
}
