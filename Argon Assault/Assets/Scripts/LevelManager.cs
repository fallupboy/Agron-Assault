using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("Menu");
        }
    }

    public void LoadStartLevel()
    {
        SceneManager.LoadScene("Level 1");
    }

    public void LoadSandboxLevel()
    {
        SceneManager.LoadScene("Sandbox");
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
