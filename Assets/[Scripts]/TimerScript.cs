using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public enum DifficultyLevel
{
    Easy    = 0,
    Medium  = 1,
    Hard    = 2
}

public class TimerScript : MonoBehaviour
{
    [SerializeField]
    public DifficultyLevel currentDifficulty;

    [SerializeField]
    public TextMeshProUGUI timerText;

    [SerializeField]
    public AnimationCurve timerCurve;
    [SerializeField]
    public AnimationCurve skillCurve;

    // Timer variables
    public float timeRemaining { get; private set; }
    public bool timerEnabled { get; private set; }

    private void OnEnable()
    {
        HackingEvents.MiniGameStart += SetupTimer;
        HackingEvents.MiniGameComplete += StopTimer;
        HackingEvents.OnAbortHack += StopTimer;
    }

    private void OnDisable()
    {
        HackingEvents.MiniGameStart -= SetupTimer;
        HackingEvents.MiniGameComplete -= StopTimer;
        HackingEvents.OnAbortHack -= StopTimer;
    }


    // Update is called once per frame
    void Update()
    {
        if (!timerEnabled) return;

        timeRemaining -= Time.deltaTime;
        CheckTimerEnd();
        UpdateTimerText();
    }

    /// Functions ///

    public void SetupTimer(DifficultyLevel difficultyLevel, PlayerSkill playerSkill)
    {
        currentDifficulty = difficultyLevel;

        timeRemaining = timerCurve.Evaluate((int)currentDifficulty) * skillCurve.Evaluate(Mathf.Clamp(playerSkill.HackingLevel, 0, 100));
        UpdateTimerText();

        StartTimer();
    }
    
    public void StartTimer()
    {
        timerEnabled = true;
    }

    public void StopTimer()
    {
        timerEnabled = false;
    }

    public void CheckTimerEnd()
    {
        if (timeRemaining <= 0.0f)
        {
            timeRemaining = 0.0f;
            HackingEvents.InvokeOnTimerDone();
            timerEnabled = false;
        }
    }

    private void UpdateTimerText()
    {
        timerText.text = GetTimerFormatted();
    }

    public string GetTimerFormatted()
    {
        int minutes = Mathf.FloorToInt(timeRemaining / 60.0f);
        int seconds = Mathf.FloorToInt(timeRemaining % 60.0f);
        int microSeconds = Mathf.FloorToInt((timeRemaining - Mathf.FloorToInt(timeRemaining)) * 1000.0f);

        return minutes.ToString("00") + ":" + seconds.ToString("00") + ":" + microSeconds.ToString("000");
    }

}

