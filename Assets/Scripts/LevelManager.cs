using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField] float loadDelay = 1f;

    AudioPlayer audioPlayer;
    Scorekeeper scorekeeper;

    static LevelManager instance;


    private void Awake()
    {
        ManageSingleton();

        audioPlayer = FindObjectOfType<AudioPlayer>();
        scorekeeper = FindObjectOfType<Scorekeeper>();
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

    private void Start()
    {
        if (scorekeeper == null)
            scorekeeper = FindObjectOfType<Scorekeeper>();
        else
            return;
    }


    public void LoadGame()
    {
        if(scorekeeper != null)
            scorekeeper.ResetScore();
        ChangeLevelMusic(1);
        SceneManager.LoadScene(1);       
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene(0);
        ChangeLevelMusic(0);
    }
    
    public void LoadGameOver()
    {
        StartCoroutine(WaitAndLoad( 2, loadDelay ));
    }

    IEnumerator WaitAndLoad(int sceneIndex, float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(sceneIndex);
        ChangeLevelMusic(sceneIndex);
    }

    void ChangeLevelMusic(int sceneIndex)
    {
        audioPlayer.ChangeSceneMusic(sceneIndex);
    }

    public void QuitGame()
    {
        Debug.Log("Quiting Game...");
        Application.Quit();
    }
}
