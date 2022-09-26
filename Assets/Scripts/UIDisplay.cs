using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIDisplay : MonoBehaviour
{

    [Header("Health")]
    [SerializeField] Slider healthSlider;
    Health playerHealth;

    [Header("Score")]
    [SerializeField] TextMeshProUGUI scoreText;
    ScoreKeeper scoreKeeper;

    void Awake()
    {
        playerHealth = FindObjectOfType<Player>().GetComponent<Health>();
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
    }


    void Start() 
    {
        healthSlider.maxValue = playerHealth.GetHealth();   
    }

    // Update is called once per frame
    void Update()
    {
        UpdateHealth();
        UpdateScore();
    }

    private void UpdateHealth()
    {
        healthSlider.value = playerHealth.GetHealth();
    }

    private void UpdateScore()
    {
        scoreText.text = scoreKeeper.GetCurrentScore().ToString("0000000");
    }
}
