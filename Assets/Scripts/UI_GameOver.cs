using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_GameOver : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI finalScoreText;
    [SerializeField] Button replayButton;
    [SerializeField] Button menuButton;

    Scorekeeper scorekeeper;
    LevelManager levelManager;

    private void Awake()
    {
        scorekeeper = FindObjectOfType<Scorekeeper>();
        levelManager = FindObjectOfType<LevelManager>();
    }

    void Start()
    {
        if (!scorekeeper)
            Debug.LogError("Scorekeeper Object Not Found");
        if (!levelManager)
            Debug.LogError("LevelManager Object Not Found");

        ButtonSetup();
    }

    private void Update()
    {
        finalScoreText.text = "Your Score:\n" + scorekeeper.GetCurrentScore();
    }

    // Set up on click listeners since they dont save the singleton LevelManager
    private void ButtonSetup()
    {
        replayButton.onClick.AddListener(levelManager.LoadGame);
        menuButton.onClick.AddListener(levelManager.LoadMainMenu);
    }
}
