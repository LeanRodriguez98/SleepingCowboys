using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_GameOver : MonoBehaviour {
    public string MainMenuSceneName;
    public void InvokeReplayLevel(float t)
    {
        Invoke("ReplayLevel", t);
    }

    public void ReplayLevel()
    {
        if (Time.timeScale != 1)
            Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void InvokeLoadScene( float t)
    {
        this.Invoke("LoadScene", MainMenuSceneName, t);
    }

    public void LoadScene(string sceneName)
    {
        if (Time.timeScale != 1)
            Time.timeScale = 1;
        SceneManager.LoadScene(sceneName);
    }
}
