using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Rendering;

public class Quiz : MonoBehaviour
{
    [SerializeField] QuestionSO question;

    [SerializeField] TextMeshProUGUI questionContainer; 

    [SerializeField] GameObject[] answerButtons;

    int correctAnswerIndex;

    [SerializeField] Sprite defaultSprite;

    [SerializeField] Sprite winSprite;

    void Start()
    {  
        correctAnswerIndex = question.GetCorrectAnswerIndex();

        questionContainer.text = question.GetQuestion();
        
        for (int i = 0; i < answerButtons.Length; i++)
        {
            TextMeshProUGUI buttonTextContainer = answerButtons[i].GetComponentInChildren<TextMeshProUGUI>();
            buttonTextContainer.text = question.GetAnswers(i);     
        }
    }

    public void OnAnswerSelected(int index)
    {
        Image buttonImage;
        
        if (index == correctAnswerIndex)
        {
            questionContainer.text = "Correct!";
            buttonImage = answerButtons[index].GetComponent<Image>();
            buttonImage.sprite = winSprite;
        }
        else
        {
            questionContainer.text = $"Incorrect. The correct answer is: {question.GetAnswers(correctAnswerIndex)}";
            buttonImage = answerButtons[correctAnswerIndex].GetComponent<Image>();
            buttonImage.sprite = winSprite;
        }
    }

}
