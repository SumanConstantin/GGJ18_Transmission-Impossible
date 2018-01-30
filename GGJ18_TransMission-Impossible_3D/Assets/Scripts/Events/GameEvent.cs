using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameEvent : MonoBehaviour
{
    // PlayerChar Events
    public delegate void PlayerCharDelegate(GameObject playerCharGO);
    public static event PlayerCharDelegate onPlayerCharHit;

    public static void PlayerCharHit(GameObject playerChar)
    {
        if (onPlayerCharHit != null)
        {
            onPlayerCharHit(playerChar);

            // Check game lose
            if(GameState.GetCurrentState().Equals(GameState.PLAYING)
                && Main.NoLivesLeft())
            {
                // Show lose scene
                GameState.SetCurrentState(GameState.LOSE, false);
            }
        }
    }
    
    // Game State Events
    public delegate void GameStateChangeDelegate(string gameState, bool isWin = false);
    public static event GameStateChangeDelegate onGameStateChanged;

    public static void GameStateChanged(string gameState, bool isWin = false)
    {
        if (onGameStateChanged != null)
        {
            onGameStateChanged(gameState, isWin);
        }

        // Change scene
        if (gameState.Equals(GameState.MAIN_MENU))
        {
            SceneManager.LoadScene("MainMenuScene", LoadSceneMode.Single);
        }
        else
        if (gameState.Equals(GameState.WIN))
        {
            SceneManager.LoadScene("WinScene", LoadSceneMode.Single);
        }
        else
        if (gameState.Equals(GameState.LOSE))
        {
            SceneManager.LoadScene("LoseScene", LoadSceneMode.Single);
        }
        else
        if (gameState.Equals(GameState.PLAYING))
        {
            SceneManager.LoadScene("GameScene", LoadSceneMode.Single);
        }
    }
}
