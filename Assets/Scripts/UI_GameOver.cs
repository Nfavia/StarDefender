using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UI_GameOver : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI finalScoreText;

    Scorekeeper scorekeeper;

    private void Awake()
    {
        scorekeeper = FindObjectOfType<Scorekeeper>();     
    }

    void Start()
    {
        if (scorekeeper == null)
            Debug.LogError("Scorekeeper Object Not Found");
        
    }

    private void Update()
    {
        finalScoreText.text = "Your Score:\n" + scorekeeper.GetCurrentScore();
    }

}
