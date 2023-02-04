using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{

    public Canvas mainMenu;
    public Canvas optionsMenu;
    public Canvas creditsMenu;

    public void Awake()
    {
        optionsMenu.gameObject.SetActive(false);
        creditsMenu.gameObject.SetActive(false);

    }

    public void NewGame()
    {
        SceneManager.LoadScene("SceneRecharge");
    }

    public void Options()
    {
        mainMenu.gameObject.SetActive(false);
        optionsMenu.gameObject.SetActive(true);

    }

    public void Back()
    {
        optionsMenu.gameObject.SetActive(false);
        creditsMenu.gameObject.SetActive(false);
        mainMenu.gameObject.SetActive(true);
    }

    public void Credits()
    {
        mainMenu.gameObject.SetActive(false);
        creditsMenu.gameObject.SetActive(true);
    }

    public void ExitGame()
    {
        Application.Quit();
        UnityEditor.EditorApplication.isPlaying = false;
    }
}
