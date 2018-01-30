using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ContinueButton : MonoBehaviour {

	public void GoToMainMenuScene()
    {
        //SceneManager.LoadScene("GameScene", LoadSceneMode.Single);
        if(GameState.GetCurrentState().Equals(GameState.MAIN_MENU))
            GameState.SetCurrentState(GameState.PLAYING, true);
        else
            GameState.SetCurrentState(GameState.MAIN_MENU, true);
    }
}
