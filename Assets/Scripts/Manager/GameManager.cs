using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    Player player;
    public LevelConfig currLevel;
    PlayerData playerData;
    private void Start()
    {
        playerData = PlayerModel.Instance.GetPlayerProfile();
        currLevel = LevelManager.Instance.levels[playerData.level-1];
    }
    public override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(gameObject);
    }
    public enum GameState
    {
        GameBegin,
        GameOver,
        CompleteLevel,
        Pause,
        Resume,
        GamePlay,
        Exited
    }
    GameState gameState;
    public void SetGameState(GameState gameState)
    {
        this.gameState = gameState;
        UpdateGameState();
    }
    void UpdateGameState()
    {
        switch (gameState)
        {
            case GameState.GameBegin:
                {
                    player = FindObjectOfType<Player>();
                    SetGameState(GameState.GamePlay);
                    UIManager.Instance.SetStateUI("WinUI", false);
                    UIManager.Instance.SetStateUI("GameOverUI", false);
                }
                break;
            case GameState.GamePlay:{
                    player.gameObject.SetActive(true);
                }break;
            case GameState.GameOver:
                {
                    UIManager.Instance.SetStateUI("GameOverUI", true);
                }break;
            case GameState.CompleteLevel:
                {
                    UIManager.Instance.SetStateUI("WinUI", true);
                    if (playerData.level < LevelManager.Instance.levels.Count)
                    {
                        if (playerData.level <= currLevel.level)
                        {
                            playerData.level++;
                            PlayerModel.Instance.SavePlayerData(playerData);
                        }
                    }
                    player.gameObject.SetActive(false);
                }
                break;
            case GameState.Pause:
                {
                    player.gameObject.SetActive(false);
                }
                break;
        }
    }
}
