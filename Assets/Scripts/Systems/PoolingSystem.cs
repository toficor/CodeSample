using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

    private void Init()
    {
        for (int i = 0; i < _poolingSystemDatas.Count; i++)
        {
            _pools.Add(_poolingSystemDatas[i].Tag,
                CreatePool(_poolingSystemDatas[i], this.transform));
        }
    }

    private Queue<GameObject> CreatePool(PoolingSystemData poolingSystemData, Transform parent)
    {
        Queue<GameObject> queue = new Queue<GameObject>();

        for (int i = 0; i < poolingSystemData.Amount; i++)
        {
            var go = InstantiatePrefabInstance(poolingSystemData, parent);
            if (!go)
            {
                break;
            }

            queue.Enqueue(go);
        }

        return queue;
    }

    public void DespawnObject(string tag, GameObject pooledGameObject)
    {
        pooledGameObject.SetActive(false);

        if (!_pools.ContainsKey(tag))
        {
            Debug.LogError("Trying enqueqe to unexisting pool ");
            return;
        }
        
        _pools[tag].Enqueue(pooledGameObject);
    }

    public GameObject SpawnObject(string tag)
    {
        if (!_pools.ContainsKey(tag))
        {
            Debug.LogError("There is no such tag in pool");
            return null;
        }

        var go = _pools[tag].Dequeue();

        if (!go)
        {
            Debug.LogWarning($@"There is no more items in pool {tag}. Instantiating new one");
            var data = _poolingSystemDatas.First(x => x.Tag == tag);
            GameObject newGo = InstantiatePrefabInstance(data, transform);
            _pools[tag].Enqueue(newGo);
        }

        go.SetActive(true);
        return go;
    }

    public GameObject SpawnObject(string tag, Vector3 position)
    {
        var go = SpawnObject(tag);
        go.transform.position = position;
        return go;
    }

    public GameObject SpawnObject(string tag, Vector3 position, Quaternion rotation)
    {
        var go = SpawnObject(tag, position);
        go.transform.rotation = rotation;
        return go;
    }

    private GameObject InstantiatePrefabInstance(PoolingSystemData poolingSystemData, Transform parent)
    {
        GameObject go = Instantiate(poolingSystemData.Prefab, parent, true);

        var iPoolable = go.GetComponent<IPoolable>();

        if (iPoolable == null)
        {
            Debug.LogError($@"This Prefab {poolingSystemData.Prefab} isn't poolable");
            return null;
        }

        iPoolable.DeSpawn += DespawnObject;
        iPoolable.Tag = poolingSystemData.Tag;
        go.SetActive(false);
        return go;
    }
}