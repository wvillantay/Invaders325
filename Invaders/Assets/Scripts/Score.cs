using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
{
    // making Score a singleton explained in the enemy script
    public static Score Instance;
    // declaring a public tmp_text to get a reference to the high score text
    public TMP_Text high_score;
    // declaring a private integer to hold the score
    private int score;

    // before the script starts
    private void Awake()
    {
        // instance equals this class
        Instance = this;
    }

    // when the script starts
    private void Start()
    {
        // setting score to 0
        score = 0;
    }

    // function to add points to the score
    public void Add(int amount)
    {
        // setting score equal to score plus the amount we passed in
        score += amount;
        // setting high scores text to the score turned into a string since it's an integer and text can only display strings
        high_score.text = string.Format("{0000}", score);
    }

    public void SendScore() // function to send the score 
    {
        EndManager.Instance.ReceiveScore(score); // sending the score 
    }
}
