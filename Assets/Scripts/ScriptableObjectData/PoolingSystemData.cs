using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PoolingSystemData", menuName = "Data/Pooling/PoolingSystemData", order = 1)]
public class PoolingSystemData : ScriptableObject
{
    public string Tag;
    public GameObject Prefab;
    public int Amount;
}
