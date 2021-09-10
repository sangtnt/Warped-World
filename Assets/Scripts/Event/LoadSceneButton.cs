using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneButton : MonoBehaviour
{
    [SerializeField]
    public string SceneName;

    public void LoadScene()
    {
        SceneManager.LoadScene(SceneName);
    }
    public void Replay()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void NextLevel()
    {
        if (GameManager.Instance.currLevel.level < LevelManager.Instance.levels.Count)
        {
            GameManager.Instance.currLevel = LevelManager.Instance.levels[GameManager.Instance.currLevel.level];
            SceneManager.LoadScene(GameManager.Instance.currLevel.sceneName);
        }
    }
}
