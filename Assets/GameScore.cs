using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameScore : MonoBehaviour {

    // This variable is update from out side of this class's code.
    // note that it is static so it belongs to the class and not an instance of that class.
    // That makes it easier to update the score from anywhere because we don't need to pass
    // GameScore reference around
    static public int score;

    private Text scoreText;
    private Text highScoreText;
    private int highScore;

	void Start () {

        score = 0;

        var c1 = transform.Find("ScoreText");
        scoreText = c1.GetComponent<Text>();

        var c2 = transform.Find("HighScoreText");
        highScoreText = c2.GetComponent<Text>();

        highScore = PlayerPrefs.GetInt("high_score");
       
        highScoreText.text = "High Score: " + highScore;

        StartCoroutine(UpdateScoreEveryTenthOfASecond());
    }

    /// <summary>
    /// Coroutine to be used instead of a MonoBehavior::Update as a premature performance optimization :).
    /// </summary>
    private IEnumerator UpdateScoreEveryTenthOfASecond() {        

        while (true)
        {
            UpdateScore();

            UpdateHighScore();

            yield return new WaitForSeconds(0.1f);
        }

    }


    private void UpdateHighScore()
    {

        if (score > highScore)
        {
            PlayerPrefs.SetInt("high_score", score);

            highScoreText.text = "High Score: " + score;
        }
    }

    private void UpdateScore()
    {
        scoreText.text = "Score: " + score;
    }
}
