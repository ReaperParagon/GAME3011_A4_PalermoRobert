using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HackingEvents
{
    /// Timer Done ///

    public delegate void OnTimerDone();

    public static event OnTimerDone TimerFinished;

    public static void InvokeOnTimerDone()
    {
        TimerFinished?.Invoke();
    }

    /// Minigame Start ///

    public delegate void OnMiniGameStart(DifficultyLevel difficultyLevel);

    public static event OnMiniGameStart MiniGameStart;

    public static void InvokeOnMiniGameStart(DifficultyLevel difficultyLevel)
    {
        MiniGameStart?.Invoke(difficultyLevel);
    }

    /// Minigame Complete ///

    public delegate void OnMiniGameComplete();

    public static event OnMiniGameComplete MiniGameComplete;

    public static void InvokeOnMiniGameComplete()
    {
        MiniGameComplete?.Invoke();
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
    }
}
