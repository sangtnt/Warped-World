using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : Singleton<LevelManager> 
{
    [SerializeField]
    public List<LevelConfig> levels;
    public override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(gameObject);
    }
}