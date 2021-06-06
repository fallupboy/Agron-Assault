using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicPlayer : MonoBehaviour
{
    private void Awake()
    {
        int musicPlayerAmount = FindObjectsOfType<MusicPlayer>().Length;
        if (musicPlayerAmount > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Update()
    {
        if (SceneManager.GetActiveScene().name == "Menu")
        {
            Destroy(gameObject);
        }
    }
}
