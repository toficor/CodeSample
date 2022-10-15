using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPoolable
{
    public string Tag { get; set; }

    public event Action<String, GameObject> DeSpawn;
}
