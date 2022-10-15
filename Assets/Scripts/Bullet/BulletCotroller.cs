using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCotroller : MonoBehaviour, IPoolable, IDestructable
{
    [SerializeField] private BulletControllerData _bulletControllerData;
    public string Tag { get; set; }
    public event Action<string, GameObject> DeSpawn;

    private void OnCollisionEnter2D(Collision2D col)
    {
        OnDestroy();
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        transform.position += transform.up * Time.deltaTime * _bulletControllerData.Speed;
    }

    public void OnDestroy()
    {
        DeSpawn?.Invoke(Tag, gameObject);
    }
}