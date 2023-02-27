using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{

    public Canvas mainMenu;
    public Canvas optionsMenu;
    public Canvas creditsMenu;
    private AudioManager audio;

    public void Awake()
    {
        optionsMenu.gameObject.SetActive(false);
        creditsMenu.gameObject.SetActive(false);
        audio = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
    }

    public void NewGame()
    {
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
//        audio.PlaySFX(0);
    }

    public void Options()
    {
        mainMenu.gameObject.SetActive(false);
        optionsMenu.gameObject.SetActive(true);
//        audio.PlaySFX(0);

    }

    public void Back()
    {
        optionsMenu.gameObject.SetActive(false);
        creditsMenu.gameObject.SetActive(false);
        mainMenu.gameObject.SetActive(true);
//        audio.PlaySFX(0);
    }

    public void Credits()
    {
        mainMenu.gameObject.SetActive(false);
        creditsMenu.gameObject.SetActive(true);
//        audio.PlaySFX(0);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
