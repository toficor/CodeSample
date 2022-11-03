using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectLoadingSystem : Singleton<EffectLoadingSystem>
{
    [SerializeField] private string _dataFolderPath;

    [SerializeField]
    private Dictionary<string, BaseBulletData> _dataDictionary = new Dictionary<string, BaseBulletData>();

    public Dictionary<string, BaseBulletData> DataDictionary => _dataDictionary;

    public override void Awake()
    {
        base.Awake();
        LoadFromResources();
    }

    private void LoadFromResources()
    {
        BaseBulletData[] dataFromResources = Resources.LoadAll<BaseBulletData>(_dataFolderPath);

        foreach (var data in dataFromResources)
        {
            if (!_dataDictionary.ContainsKey(data.Tag))
            {
                _dataDictionary.Add(data.Tag, data);
            }
        }
    }

    public BaseBulletData TryGetData(string tag)
    {
        BaseBulletData data;
        if (_dataDictionary.TryGetValue(tag, out data))
        {
            return data;
        }
        Debug.LogError("There is no such data in dictionary");
        return null;
    }
}