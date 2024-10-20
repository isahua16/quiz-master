using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Rendering;
using System;
using Random = UnityEngine.Random;

public class Quiz : MonoBehaviour
{
    [SerializeField] List<QuestionSO> questions = new List<QuestionSO>();
    QuestionSO currentQuestion;
    [SerializeField] TextMeshProUGUI questionTextContainer; 
    [SerializeField] GameObject[] answerButtons;
    int correctAnswerIndex;
    [SerializeField] Sprite defaultSprite;
    [SerializeField] Sprite winSprite;
    [SerializeField] Timer timer;
    [SerializeField] TextMeshProUGUI scoreTextContainer;
    Score score;
    bool hasAnsweredEarly;
    [SerializeField] Slider progressBar;
    public bool isComplete = false;

    void Awake()
    {
        score = FindObjectOfType<Score>();
        timer.GetComponent<Timer>();
    }

    void Start()
    {
        progressBar.maxValue = questions.Count;
        GetRandomQuestion();
        DisplayQuestion();
    }

    void Update()
    {
        if(timer.loadNextQuestion)
        {
            if(progressBar.value == progressBar.maxValue)
            {
                isComplete = true;
                return;
            }
            
            hasAnsweredEarly = false;
            GetNextQuestion();
            timer.loadNextQuestion = false;
        }
        else if (!hasAnsweredEarly && !timer.isAnsweringQuestion)
        {
            ShowAnswer();
        }
    }

    private void DisplayQuestion()
    {
        correctAnswerIndex = currentQuestion.GetCorrectAnswerIndex();
        questionTextContainer.text = currentQuestion.GetQuestion();
        score.IncrementQuestionsSeen();
        progressBar.value++;


        for (int i = 0; i < answerButtons.Length; i++)
        {
            TextMeshProUGUI buttonTextContainer = answerButtons[i].GetComponentInChildren<TextMeshProUGUI>();
            buttonTextContainer.text = currentQuestion.GetAnswers(i);
        }
    }

    public void GetNextQuestion()
    {
        if(questions.Count > 0)
        {
            SetAnswerButtonsState(true);
            SetDefaultButtonSprites();
            GetRandomQuestion();
            DisplayQuestion();
        }
    }

    private void GetRandomQuestion()
    {
        int index = Random.Range(0, questions.Count);
        currentQuestion = questions[index];

        if(questions.Contains(currentQuestion))
        {
            questions.Remove(currentQuestion);
        }
    }

    public void OnAnswerSelected(int index)
    {        
        hasAnsweredEarly = true;
        Image buttonImage;
        if (index == correctAnswerIndex)
        {
            questionTextContainer.text = "Correct!";
            buttonImage = answerButtons[index].GetComponent<Image>();
            buttonImage.sprite = winSprite;
            score.IncrementCorrectAnswers();
        }
        else
        {
            questionTextContainer.text = $"Incorrect. The correct answer is: {currentQuestion.GetAnswers(correctAnswerIndex)}";
            buttonImage = answerButtons[correctAnswerIndex].GetComponent<Image>();
            buttonImage.sprite = winSprite;
        }
        SetAnswerButtonsState(false);
        timer.CancelTimer();
        scoreTextContainer.text = $"Score: {score.CalculateScore()}%";
        
        if(progressBar.value == progressBar.maxValue)
        {
            isComplete = true;
        }
    }

    private void SetAnswerButtonsState(bool state)
    {
        for (int i = 0; i < answerButtons.Length; i++)
        {
            Button button = answerButtons[i].GetComponent<Button>();
            button.interactable = state;
        }
    }

    private void SetDefaultButtonSprites()
    {         
         for (int i = 0; i < answerButtons.Length; i++)
         {
            Image buttonImage = answerButtons[i].GetComponent<Image>();
            buttonImage.sprite = defaultSprite;
         }
    }

    void ShowAnswer()
    {
        questionTextContainer.text = $"You're out of time. The answer is: {currentQuestion.GetAnswers(correctAnswerIndex)}";
        Image buttonImage = answerButtons[correctAnswerIndex].GetComponent<Image>();
        buttonImage.sprite = winSprite;
        SetAnswerButtonsState(false);
        scoreTextContainer.text = $"Score: {score.CalculateScore()}%";
    }
}
