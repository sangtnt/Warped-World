using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelMenu : MonoBehaviour
{
    [SerializeField]
    GameObject listOfLevel;
    [SerializeField]
    GameObject levelPrefab;

    public PlayerData player;
    // Start is called before the first frame update
    void Start()
    {
        player = PlayerModel.Instance.GetPlayerProfile();
        foreach (LevelConfig levelConfig in LevelManager.Instance.levels)
        {
            GameObject newLevelItem = Instantiate(levelPrefab, listOfLevel.transform);
            newLevelItem.GetComponent<LevelItemSetup>().SetLevelInfo(levelConfig, player.level);
        }
    }
}
