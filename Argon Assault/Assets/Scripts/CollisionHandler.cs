using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Playables;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] int delayBeforeRestart = 2;
    [SerializeField] ParticleSystem explosionParticles;
    [SerializeField] PlayableDirector playableDirector;

    void OnTriggerEnter(Collider other)
    {
        StartCoroutine(ProceedCrash());
    }

    IEnumerator ProceedCrash()
    {
        explosionParticles.Play();
        GetComponent<PlayerControl>().enabled = false;
        GetComponent<MeshRenderer>().enabled = false;
        playableDirector.Stop();
        yield return new WaitForSeconds(delayBeforeRestart);
        ReloadLevel();
    }

    void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
}
