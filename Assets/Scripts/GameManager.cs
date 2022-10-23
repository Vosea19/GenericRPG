using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //Load and unload Options Menu Scene
    private string cancel = "Cancel";
    private string optionsScene = "Options";
    //
    private void Awake()
    {
        DontDestroyOnLoad(transform);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown(cancel))
        {
            Scene currentScene = SceneManager.GetSceneByName(optionsScene);
            if (currentScene.name == optionsScene)
            {
                SceneManager.UnloadSceneAsync(optionsScene);
            }
            else SceneManager.LoadScene(optionsScene,LoadSceneMode.Additive); 
        }
    }
}
