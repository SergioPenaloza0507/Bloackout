using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameObjectPool : MonoBehaviour
{
    [SerializeField] private GameObject[] prefabs;
    [SerializeField] private int stockCount;
    [SerializeField] private bool initializeOnAwake;
    

    private List<GameObject>[] stock;
    private void Awake()
    {
        if(initializeOnAwake)
            FillStock();
    }

    private void FillStock()
    {
        stock = new List<GameObject>[prefabs.Length];
        for (int i = 0; i < prefabs.Length; i++)
        {
            var list = new List<GameObject>();
            for (int j = 0; j < stockCount; j++)
            {
                list.Add(SpawnObject(i));
            }
            stock[i] = list;
        }
    }

    private GameObject SpawnObject(int index)
    {
        if (!CheckPrefabRange(index)) return null;
        GameObject instance = Instantiate(prefabs[index], Vector3.one * -10000, Quaternion.identity);
        instance.SetActive(false);
        return instance;
    }

    private bool CheckPrefabRange(int index)
    {
        return prefabs != null && prefabs.Length > 0 && index >= 0 && index < prefabs.Length;
    }

    public TComponent AllocateObject<TComponent>(int index, Vector3? position = null, Quaternion? rotation = null, Vector3? scale = null) where TComponent : Component
    {
        if (!CheckPrefabRange(index)) return null;
        GameObject go = null;
        var list = stock[index];
        if (list.Count <= 0)
        {
            go = SpawnObject(index);
            list.Add(go);
        }
        else
        {
            
            go = list.FirstOrDefault(x => !x.activeSelf) ?? SpawnObject(index);
        }

        Transform goTransform = go.transform;
        if (position != null)
        {
            goTransform.position = position.Value;
        }

        if (rotation != null)
        {
            goTransform.rotation = rotation.Value;
        }

        if (scale != null)
        {
            goTransform.localScale = scale.Value;
        }
        
        return go.GetComponent<TComponent>();
    }
}
