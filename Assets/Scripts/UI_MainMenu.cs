using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_MainMenu : MonoBehaviour
{
    [SerializeField] Button startButton;
    [SerializeField] Button quitButton;

    LevelManager levelManager;

    private void Awake()
    {
        levelManager = FindObjectOfType<LevelManager>();
    }

    // Start is called before the first frame update
    void Start()
    {
        if(!levelManager)
        {
            levelManager = FindObjectOfType<LevelManager>();
        }    

        if (!startButton || !quitButton)
            Debug.LogError("Buttons Not Found");

        ButtonSetup();
    }

    private void ButtonSetup()
    {
        startButton.onClick.RemoveAllListeners();
        quitButton.onClick.RemoveAllListeners();

        startButton.onClick.AddListener(levelManager.LoadGame);
        quitButton.onClick.AddListener(levelManager.QuitGame);
    }
}
