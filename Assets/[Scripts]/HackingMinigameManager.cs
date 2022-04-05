using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class HackingMinigameManager : MonoBehaviour
{
    public GameObject resultsScreen;
    public TextMeshProUGUI resultsText;
    public TextMeshProUGUI message;

    private void OnEnable()
    {
        HackingEvents.MiniGameStart += Setup;
        HackingEvents.MiniGameComplete += GameComplete;
        HackingEvents.TimerFinished += OutOfTime;
        HackingEvents.OnAbortHack += HackingAbort;
    }

    private void OnDisable()
    {
        HackingEvents.MiniGameStart -= Setup;
        HackingEvents.MiniGameComplete -= GameComplete;
        HackingEvents.TimerFinished -= OutOfTime;
        HackingEvents.OnAbortHack -= HackingAbort;
    }

    public void StartMinigame(DifficultyLevel difficulty)
    {
        HackingEvents.InvokeOnMiniGameStart(difficulty);
    }

    public void StartMinigame(int difficulty)
    {
        HackingEvents.InvokeOnMiniGameStart((DifficultyLevel)difficulty);
    }

    private void GameComplete()
    {
        DisplayResults("Hacking Successful!");
    }

    private void OutOfTime()
    {
        DisplayResults("You Ran Out of Time!");
    }

    private void HackingAbort()
    {
        DisplayResults("Hacking Aborted!");
    }

    private void DisplayResults(string message)
    {
        print(message);
        resultsText.text = message;
        resultsScreen.SetActive(true);
    }

    private void Setup(DifficultyLevel _)
    {
        resultsScreen.SetActive(false);
    }

    public void DisplayMessage(string msg)
    {
        message.text = msg;
        message.GetComponent<Animator>().SetTrigger("AnimTrigger");
    }

    /// Input System ///

    private void OnAbort(InputValue value)
    {
        HackingEvents.InvokeOnAbortHack();
    }
}
