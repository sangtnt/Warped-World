using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class LevelConfig : ScriptableObject
{
    public int level;
    public int numOfPowersToWin;
    public string sceneName;
    public int boss;
}
