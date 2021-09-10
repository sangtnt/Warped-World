using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerModel : Singleton<PlayerModel>
{
    private PlayerData player;
    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }
    public void CreateNewPlayer(PlayerData player)
    {
        this.player = player;
        SavePlayerData(player);
    }
    public void SavePlayerData(PlayerData player)
    {
        string path = Application.persistentDataPath + "/Player.json";
        this.player = player;
        string jsonData = JsonUtility.ToJson(player);
        System.IO.File.WriteAllText(path, jsonData);
    }
    public PlayerData GetPlayerProfile()
    {
        string path = Application.persistentDataPath + "/Player.json";
        if (System.IO.File.Exists(path))
        {
            string json = System.IO.File.ReadAllText(path);
            try
            {
                player = JsonUtility.FromJson<PlayerData>(json);
                return player;
            }
            catch
            {
                Debug.LogWarning("Load Profile Error!");
                return null;
            }
        }
        else
        {
            player = new PlayerData();
            player.level = 1;
            PlayerModel.Instance.CreateNewPlayer(player);
            return player;
        }
    }
}
