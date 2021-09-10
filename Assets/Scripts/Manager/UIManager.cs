using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    [SerializeField]
    public List<GameObject> listOfUI = new List<GameObject>();
    public override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(gameObject);
    }
    public void SetStateUI(string UIName, bool state)
    {
        GameObject uIAsset = GetUIByName(UIName);
        uIAsset.SetActive(state);
    }
    // GetUIByType
    public GameObject GetUIByName(string UIName)
    {
        foreach(GameObject uIAsset in listOfUI)
        {
            if(uIAsset.name == UIName)
            {
                return uIAsset;
            }
        }
        return null;
    }
}
