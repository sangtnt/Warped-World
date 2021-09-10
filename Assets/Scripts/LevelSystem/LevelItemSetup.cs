using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelItemSetup : MonoBehaviour
{
    LevelConfig levelConfig;
    [SerializeField]
    Text levelText;
    public void SetLevelInfo(LevelConfig levelConfig, int playerLevel)
    {
        this.levelConfig = levelConfig;
        levelText.text = levelConfig.level.ToString();
        if (playerLevel >= levelConfig.level)
        {
            gameObject.GetComponent<Button>().interactable = true;
        }
        else
        {
            gameObject.GetComponent<Button>().interactable = false;
        }
    }

    public void LoadLevel()
    {
        SceneManager.LoadScene(levelConfig.sceneName);
        GameManager.Instance.currLevel = levelConfig;
    }
}
