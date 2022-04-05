using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HackingEvents
{
    /// Minigame End ///

    public delegate void OnMinigameEndEvent();

    public static event OnMinigameEndEvent OnMinigameEnd;


    /// Timer Done ///

    public delegate void OnTimerDone();

    public static event OnTimerDone TimerFinished;

    public static void InvokeOnTimerDone()
    {
        TimerFinished?.Invoke();
        OnMinigameEnd?.Invoke();
    }

    /// Minigame Start ///

    public delegate void OnMiniGameStart(DifficultyLevel difficultyLevel, PlayerSkill playerSkill);

    public static event OnMiniGameStart MiniGameStart;

    public static void InvokeOnMiniGameStart(DifficultyLevel difficultyLevel, PlayerSkill playerSkill)
    {
        MiniGameStart?.Invoke(difficultyLevel, playerSkill);
    }

    /// Minigame Complete ///

    public delegate void OnMiniGameComplete();

    public static event OnMiniGameComplete MiniGameComplete;

    public static void InvokeOnMiniGameComplete()
    {
        MiniGameComplete?.Invoke();
        OnMinigameEnd?.Invoke();
    }

    /// Connection Event ///

    public delegate void OnConnectionEvent();

    public static event OnConnectionEvent OnConnection;

    public static void InvokeOnConnection()
    {
        OnConnection?.Invoke();
    }

    /// Hacking Aborted Event ///

    public delegate void OnAbortHackEvent();

    public static event OnAbortHackEvent OnAbortHack;

    public static void InvokeOnAbortHack()
    {
        OnAbortHack?.Invoke();
        OnMinigameEnd?.Invoke();
    }
}
