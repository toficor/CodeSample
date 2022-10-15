using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class PoolingSystem : Singleton<PoolingSystem>
{
    [SerializeField] private List<PoolingSystemData> _poolingSystemDatas = new List<PoolingSystemData>();

    private Dictionary<string, Queue<GameObject>> _pools = new Dictionary<string, Queue<GameObject>>();

    public override void Awake()
    {
        base.Awake();
        Init();
    }

    //przekminic czy nie zrobic interfejsu dla inicjalizowanych elementow
    private void Init()
    {
        for (int i = 0; i < _poolingSystemDatas.Count; i++)
        {
            GameObject poolParent = new GameObject(_poolingSystemDatas[i].Tag);
            poolParent.transform.SetParent(this.transform);
            _pools.Add(_poolingSystemDatas[i].Tag,
                CreatePool(_poolingSystemDatas[i].Amount, _poolingSystemDatas[i].Prefab, poolParent.transform));
        }
    }

    private Queue<GameObject> CreatePool(int amount, GameObject prefab, Transform parent)
    {
        Queue<GameObject> queue = new Queue<GameObject>();

        for (int i = 0; i < amount; i++)
        {
            GameObject go = Instantiate(prefab, parent, true);
            go.SetActive(false);
            queue.Enqueue(go);
        }

        return queue;
    }

    public GameObject SpawnObject(string tag)
    {
        return null;
    }

    public GameObject SpawnObjectAtPoint(string tag, Vector3 position)
    {
        return null;
    }
}