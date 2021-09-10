using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadLevelConfig : LoadSceneConfig
{
    public override void Start()
    {
        base.Start();
        GameManager.Instance.SetGameState(GameManager.GameState.GameBegin);
    }
    private void OnDestroy()
    {
        GameManager.Instance.SetGameState(GameManager.GameState.Exited);
    }
}
