﻿using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour
{
    int scoreP1 = 0, scoreP2 = 0;
    public TextMeshProUGUI tmp1, tmp2;
    public int scoreThreshold = 3;
    public TextMeshProUGUI winText;
    private bool gameHasBeenWon;

    public void IncreaseScore(bool playerOne)
    {
        if (playerOne)
            scoreP1++;
        else
            scoreP2++;

        DisplayScore();
        CheckForWinner();
    }

    private void Update()
    {
        if (gameHasBeenWon && Input.GetKeyDown(KeyCode.P))
        {
            SceneManager.LoadScene(0);
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            IncreaseScore(false);
        }
    }

    private void CheckForWinner()
    {
        if (scoreP1 >= scoreThreshold)
            SetWinner(true);

        if (scoreP2 >= scoreThreshold)
            SetWinner(false);

    }

    private void SetWinner(bool playerOne)
    {
        if (!gameHasBeenWon)
        {
            winText.gameObject.SetActive(true);
            if (playerOne)
                winText.text = "Player 1 Wins!!!";
            else
                winText.text = "Player 2 Wins!!!";
            gameHasBeenWon = true;
        }
    }

    private void DisplayScore()
    {
        tmp1.text = "Sets completed: " + scoreP1;
        tmp2.text = "Sets completed: " + scoreP2;
    }

    public void ResetScore()
    {
        scoreP1 = scoreP2 = 0;
        DisplayScore();
    }
}
