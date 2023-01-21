using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    [Header("Shooting")]
    [SerializeField] AudioClip shootingClip;
    [SerializeField] [Range(0f,1f)] float shootingVolume;

    [Header("Damage")]
    [SerializeField] AudioClip damageTakenClip;
    [SerializeField][Range(0f, 1f)] float damageVolume;

    [Header("Scene Music")]
    [SerializeField] AudioClip menuMusic;
    [SerializeField] AudioClip gameMusic;
    [SerializeField] AudioClip gameOverMusic;

    AudioSource myAudioSource;
    static AudioPlayer instance;

    private void Awake()
    {
        ManageSingleton();

        myAudioSource = GetComponent<AudioSource>();
    }

    void ManageSingleton()
    {
        if (instance != null)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }


    public void PlayShootingClip()
    {
        if (shootingClip != null)
        {
            PlayClip(shootingClip, shootingVolume);
        }
    }

    public void PlayDamageClip()
    {
        if (damageTakenClip != null)
        {
            PlayClip(damageTakenClip, damageVolume);
        }
    }

    public void ChangeSceneMusic(int sceneIndex)
    {
        if (sceneIndex == 0)
            myAudioSource.clip = menuMusic;
        else if (sceneIndex == 1)
            myAudioSource.clip = gameMusic;
        else if (sceneIndex == 2)
            myAudioSource.clip = gameOverMusic;

        myAudioSource.Play();
    }

    private void PlayClip(AudioClip clip, float volume)
    {
        if(clip != null)
        {
            Vector3 cameraPos = Camera.main.transform.position;
            AudioSource.PlayClipAtPoint(clip, cameraPos, volume);
        }
    }

}
