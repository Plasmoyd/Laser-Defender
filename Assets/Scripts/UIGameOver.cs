using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIGameOver : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI scoreText;
    ScoreKeeper scoreKeeper;

    void Awake()
    {
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
    }

    void Start()
    {
        if(scoreKeeper != null)
        {
            scoreText.text = "Your score : " + scoreKeeper.GetCurrentScore().ToString("0000000");
        }
        
    }

    
}
