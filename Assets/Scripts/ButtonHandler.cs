using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonHandler : MonoBehaviour
{
    private string mainMenuScene = "MainMenu";
    public void ExitGame()
    {
        Application.Quit();
    }
    public void ExitToMain()
    {
        SceneManager.LoadScene(mainMenuScene,LoadSceneMode.Single);
    }
    public void LoadGame()
    {

    }
    public void NewGame()
    {

    }
}
