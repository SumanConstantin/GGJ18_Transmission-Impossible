using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState {

    public const string MAIN_MENU = "MAIN_MENU";
    public const string WIN = "WIN";
    public const string LOSE = "LOSE";
    public const string PLAYING = "PLAYING";
    public const string PLAYING_BOSS = "PLAYING_BOSS";
    public const string PLAYING_BOSS_DEFEATED = "PLAYING_BOSS_DEFEATED";
    public const string PLAYING_DEFEATED = "PLAYING_DEFEATED";
    public const string PAUSED = "PAUSED";

    private static string currentState = MAIN_MENU;
    private static bool currentIsWin = false;

    public static string GetCurrentState() { return currentState; }
    public static bool GetCurrentIsWin() { return currentIsWin; }

    public static void SetCurrentState(string state, bool isWin = false)
    {
        currentState = state;
        currentIsWin = isWin;
        GameEvent.GameStateChanged(currentState, currentIsWin);
    }
}
