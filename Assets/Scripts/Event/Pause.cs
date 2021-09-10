using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    public void PauseGame()
    {
        UIManager.Instance.SetStateUI("PauseUI", true);
        Time.timeScale = 0;
        GameManager.Instance.SetGameState(GameManager.GameState.Pause);
    }
    public void ResumeGame()
    {
        UIManager.Instance.SetStateUI("PauseUI", false);
        Time.timeScale = 1;
        GameManager.Instance.SetGameState(GameManager.GameState.GamePlay);
    }
    private void OnDestroy()
    {
        Time.timeScale = 1;
        UIManager.Instance.SetStateUI("PauseUI", false);
    }
}
