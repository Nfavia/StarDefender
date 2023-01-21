using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UI_Display : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] Slider healthBar;
    [SerializeField] Health playerHealth;

    [Header("Score")]
    [SerializeField] TextMeshProUGUI scoreText;
    Scorekeeper scorekeeper;



    private void Awake()
    {
        scorekeeper = FindObjectOfType<Scorekeeper>();
    }

    void Start()
    {
        healthBar.maxValue = playerHealth.GetHealth();
        healthBar.value = healthBar.maxValue;
    }

    void Update()
    {
        DisplayHealth();
        DisplayScore();
    }

    void DisplayHealth()
    {
        if (healthBar != null)
            healthBar.value = playerHealth.GetHealth();
    }

    void DisplayScore()
    {
        if (scorekeeper != null)
            scoreText.text = scorekeeper.GetCurrentScore().ToString("000000000");
    }

}
