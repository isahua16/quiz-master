using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    [SerializeField] float timeToAnswer = 10f;
    [SerializeField] float timeToNextQuestion = 5f;
    float timeRemaining;
    [SerializeField] GameObject timer;
    Image timerImage;
    public bool isAnsweringQuestion = false;
    public bool loadNextQuestion;
    
    void Start()
    {
        StartTimer();
    }

    void StartTimer()
    {
        timeRemaining = timeToAnswer;
        timerImage = timer.GetComponent<Image>();
        timerImage.fillAmount = 1;
        isAnsweringQuestion = true;
    }

    void Update()
    {
        UpdateTimer();
    }

    void UpdateTimer()
    {
        timeRemaining -= Time.deltaTime;

        if(isAnsweringQuestion)
        {            
            if(timeRemaining > 0)
            {
                timerImage.fillAmount = timeRemaining / timeToAnswer;
            }
            else
            {
                isAnsweringQuestion = false;
                timeRemaining = timeToNextQuestion;
                timerImage.fillAmount = 1;
            }
        }
        else
        {            
            if(timeRemaining > 0)
            {   
                timerImage.fillAmount = timeRemaining / timeToNextQuestion;
            }            
            else
            {
                loadNextQuestion = true;
                isAnsweringQuestion = true;
                timeRemaining = timeToAnswer;
                timerImage.fillAmount = 1;
            }
        }
    }

    public void CancelTimer()
    {
        timeRemaining = 0f;
    }
}


