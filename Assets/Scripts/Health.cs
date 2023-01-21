using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] bool isPlayer = false;
    [SerializeField] int health;
    [SerializeField] ParticleSystem hitEffect;

    [SerializeField] bool applyCameraShake = false;
    CameraShake cameraShake;

    [Header("Enemy Only")]
    [SerializeField] int score = 50;

    AudioPlayer audioPlayer;
    Scorekeeper scoreKeeper;
    LevelManager levelManager;

    private void Awake()
    {
        levelManager = FindObjectOfType<LevelManager>();
        scoreKeeper = FindObjectOfType<Scorekeeper>();
        audioPlayer = FindObjectOfType<AudioPlayer>();
        cameraShake = Camera.main.GetComponent<CameraShake>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        DamageDealer damageDealer = collision.GetComponent<DamageDealer>();

        if (damageDealer != null)
        {
            TakeDamage(damageDealer.GetDamage());
            PlayHitEffect();
            //make only for player?
            audioPlayer.PlayDamageClip();
            ShakeCamera();          
            damageDealer.Hit();
        }
    }

    void ShakeCamera()
    {
        if(cameraShake != null && applyCameraShake)
            cameraShake.Play();
    }

    void TakeDamage(int damage)
    { 
        health -= damage;

        if (health <= 0)
        {
            if(!isPlayer)
            {
                scoreKeeper.ChangeScore(score);
            }
            else
            {
                levelManager.LoadGameOver();
                Debug.Log("Game Over");
            }
            Die();
        }
    }

    void PlayHitEffect()
    {
        if(hitEffect != null)
        {
            ParticleSystem inst = Instantiate(hitEffect, transform.position, Quaternion.identity);
            Destroy(inst.gameObject, inst.main.duration + inst.main.startLifetime.constantMax);
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }

    public int GetHealth()
    {
        return health;
    }

}
