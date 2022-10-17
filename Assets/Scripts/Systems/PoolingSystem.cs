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
            //  GameObject poolParent = new GameObject(_poolingSystemDatas[i].Tag);
            //  poolParent.transform.SetParent(this.transform);
            _pools.Add(_poolingSystemDatas[i].Tag,
                CreatePool(_poolingSystemDatas[i].Amount, _poolingSystemDatas[i].Prefab, this.transform,
                    _poolingSystemDatas[i].Tag));
        }
    }

    private Queue<GameObject> CreatePool(int amount, GameObject prefab, Transform parent, string tag)
    {
        Queue<GameObject> queue = new Queue<GameObject>();

        for (int i = 0; i < amount; i++)
        {
            GameObject go = Instantiate(prefab, parent, true);

            //debug
            go.name += i;

            var iPoolable = go.GetComponent<IPoolable>();

            if (iPoolable == null)
            {
                Debug.LogError($@"This Prefab {prefab.name} isn't poolable");
                break;
            }

            iPoolable.DeSpawn += DespawnObject;
            iPoolable.Tag = tag;
            go.SetActive(false);
            queue.Enqueue(go);
        }

        return queue;
    }

    public void DespawnObject(string tag, GameObject pooledGameObject)
    {
        pooledGameObject.SetActive(false);
        //pozmieniac potem wszystkie odowlania do ementow w dictionary na TryGet albo check name czy tam key
        _pools[tag].Enqueue(pooledGameObject);
    }

    public GameObject SpawnObject(string tag)
    {
        var go = _pools[tag].Dequeue();
        //dodac instancje jesli nie ma juz w poolu nic
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
}