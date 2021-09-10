using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : Singleton<PoolManager>
{
    [SerializeField]
    List<PoolObject> poolObjects = new List<PoolObject>();
    [SerializeField]
    GameObject poolContainer;
    public override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
        foreach(PoolObject poolObject in poolObjects)
        {
            FillPoolObjectList(poolObject);
        }
    }
    void FillPoolObjectList(PoolObject poolObject)
    {
        for(int i = 0; i<poolObject.amount; i++)
        {
            GameObject newGO = Instantiate(poolObject.prefab, poolContainer.transform);
            newGO.SetActive(false);
            poolObject.gameObjects.Add(newGO);
        }
    }
    public GameObject GetGameObjectByPoolType(PoolType type)
    {
        PoolObject poolObject = GetPoolObjectByPoolType(type);
        GameObject returnGameObject;
        if (poolObject.gameObjects.Count > 0)
        {
            returnGameObject = poolObject.gameObjects[poolObject.gameObjects.Count - 1];
            poolObject.gameObjects.Remove(returnGameObject);
        }
        else
        {
            returnGameObject = Instantiate(poolObject.prefab, poolContainer.transform);
        }
        returnGameObject.SetActive(true);
        return returnGameObject;
    }
    public void DeactivateGameObject(GameObject GO, PoolType type)
    {
        PoolObject poolObject = GetPoolObjectByPoolType(type);
        GO.SetActive(false);
        if (!poolObject.gameObjects.Contains(GO))
        {
            poolObject.gameObjects.Add(GO);
        }
    }
    PoolObject GetPoolObjectByPoolType(PoolType type)
    {
        foreach (PoolObject poolObject in poolObjects)
        {
            if (poolObject.type == type)
            {
                return poolObject;
            }
        }
        return null;
    }
}
