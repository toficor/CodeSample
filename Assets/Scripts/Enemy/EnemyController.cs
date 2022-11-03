using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour, IPoolable, IDestructable
{
    public string Tag { get; set; }
    public event Action<string, GameObject> DeSpawn;

    private void OnCollisionEnter2D(Collision2D col)
    {
        OnDestruct();
    }

    public void OnDestruct()
    {
        DeSpawn?.Invoke(Tag, gameObject);
    }
}