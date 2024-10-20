using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EndScreen : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI finalScoreContainer;
    Score score;

    void Awake()
    {
        score = FindObjectOfType<Score>();
    }

    public void UpdateFinalScore()
    {
        finalScoreContainer.text = $"Congratulations!\nYou scored: {score.CalculateScore()}%";
    }


}
