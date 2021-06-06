using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] GameObject hitVFX;
    [SerializeField] GameObject explosionFX;
    [SerializeField] int healthPoints = 100;
    [SerializeField] int scoreAfterDestroy = 35;

    ScoreBoard scoreBoard;
    PlayerControl playerControl;
    GameObject parentVFXGameObject;


    void Start()
    {
        AddRigidbody();
        scoreBoard = FindObjectOfType<ScoreBoard>();
        playerControl = FindObjectOfType<PlayerControl>();
        parentVFXGameObject = GameObject.FindWithTag("SpawnAtRuntime");
    }

    void AddRigidbody()
    {
        Rigidbody rb = gameObject.AddComponent<Rigidbody>();
        rb.useGravity = false;
    }

    void OnParticleCollision(GameObject other)
    {
        if (other.gameObject.CompareTag("Player Laser"))
        {
            TakeDamage(playerControl.GetPlayerLaserDamage());
        }
    }

    public void TakeDamage(int amount)
    {
        GameObject vfx = Instantiate(hitVFX, transform.position, Quaternion.identity);
        vfx.transform.parent = parentVFXGameObject.transform;
        healthPoints -= amount;
        if (healthPoints <= 0)
        {
            scoreBoard.IncreaseScore(scoreAfterDestroy);
            DestroyObject();
        }
    }

    void DestroyObject()
    {
        GameObject fx = Instantiate(explosionFX, transform.position, Quaternion.identity);
        fx.transform.parent = parentVFXGameObject.transform;
        Destroy(gameObject);
    }
}
