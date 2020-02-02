using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour
{
    int scoreP1 = 0, scoreP2 = 0;
    public ScoreTracker st1, st2;
    public int scoreThreshold = 3;
    public TextMeshProUGUI winText;
    private bool gameHasBeenWon;

    public void IncreaseScore(bool playerOne)
    {
        if (playerOne)
        {
            scoreP1++;
            st1.IterateScore(scoreP1);
        }
        else
        {
            scoreP2++;
            st2.IterateScore(scoreP2);

        }

        CheckForWinner();
    }

    private void Update()
    {
        if (gameHasBeenWon && Input.GetKeyDown(KeyCode.P))
        {
            SceneManager.LoadScene(0);
        }

        if (Input.GetKeyDown(KeyCode.I))
        {
            IncreaseScore(true);
        }

        //if (Input.GetKeyDown(KeyCode.O))
        //{
        //    IncreaseScore(false);
        //}
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
                winText.text = "Harry Wins!!!";
            else
                winText.text = "Gary Wins!!!";
            gameHasBeenWon = true;
        }
    }

    public void ResetScore()
    {
        scoreP1 = scoreP2 = 0;
        st1.TurnOffAllSprites();
        st2.TurnOffAllSprites();
    }
}
