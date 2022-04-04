using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreScript : MonoBehaviour
{
    [Header("UI")]
    public Slider progressBar;
    [SerializeField]
    private TextMeshProUGUI scoreUI;
    [SerializeField]
    private GameObject scoreAddUI;
    public float animationTime = 1.0f;
    public AnimationCurve tickUpCurve;

    public int Score { get; private set; }
    private float tickUpScore;
    private int tempAddingScore;
    private GameObject tempAddScoreObject;
    public AnimationCurve ScoreGoal;

    private IEnumerator AnimateAddScoreCoroutine_Ref = null;
    private bool GameOver = false;

    private void OnEnable()
    {
        MatchThreeEvents.AddScore += AddScore;
        MatchThreeEvents.MiniGameStart += Setup;
        MatchThreeEvents.TimerFinished += TimerGameOver;
    }

    private void OnDisable()
    {
        MatchThreeEvents.AddScore -= AddScore;
        MatchThreeEvents.MiniGameStart -= Setup;
        MatchThreeEvents.TimerFinished -= TimerGameOver;
    }

    private void TimerGameOver()
    {
        GameOver = true;
    }

    private void AddScore(int score)
    {
        if (GameOver) return;

        score *= score;
        Score += score;
        progressBar.value = Score;

        // Check for win
        if (progressBar.value >= progressBar.maxValue)
        {
            GameOver = true;
            MatchThreeEvents.InvokeOnMiniGameComplete();
        }

        int scoreToAdd = score;

        if (AnimateAddScoreCoroutine_Ref != null)
        {
            StopCoroutine(AnimateAddScoreCoroutine_Ref);
            Destroy(tempAddScoreObject);
            scoreToAdd = score + tempAddingScore;
        }

        tempAddingScore = scoreToAdd;
        AnimateAddScoreCoroutine_Ref = AnimateAddScore(scoreToAdd);
        StartCoroutine(AnimateAddScoreCoroutine_Ref);
    }

    private IEnumerator AnimateAddScore(int score)
    {
        // Display added score
        tempAddScoreObject = Instantiate(scoreAddUI, scoreUI.transform);
        tempAddScoreObject.GetComponent<TextMeshProUGUI>().text = "+" + score;

        yield return new WaitForSeconds(animationTime);
        Destroy(tempAddScoreObject);

        tickUpScore = float.Parse(scoreUI.text);

        // Tick up score UI to the added score total
        for (float t = 0; tickUpScore < Score; tickUpScore += 1)
        {
            t += 0.1f;
            scoreUI.text = ((int)tickUpScore).ToString();
            yield return new WaitForSeconds(tickUpCurve.Evaluate(t));
        }

        scoreUI.text = Score.ToString();

        AnimateAddScoreCoroutine_Ref = null;
    }

    private void Setup(DifficultyLevel difficulty)
    {
        progressBar.maxValue = ScoreGoal.Evaluate((int)difficulty);
        progressBar.value = 0;

        GameOver = false;

        Score = 0;
        scoreUI.text = "0";
        if (tempAddScoreObject != null)
            Destroy(tempAddScoreObject);
    }

}
