using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class BulletCotroller : MonoBehaviour, IPoolable, IDestructable
{
    [SerializeField] private BaseBulletData _baseBulletData;
    [SerializeField] private ChainBulletData _chainBulletData;
    [SerializeField] private PiercingBulletData _piercingBulletData;
    [SerializeField] private ForkBulletData _forkBulletData;
    private int _piercingCounter = 0;
    private float _autoDestroyTimer = 0;

    public BulletProperties BulletProperties;
    public string Tag { get; set; }
    public event Action<string, GameObject> DeSpawn;

    private void OnEnable()
    {
        _piercingCounter = 0;
        _autoDestroyTimer = 0;
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (BulletProperties.Equals(BulletProperties.None))
        {
            Debug.LogWarning("None");
            OnDestroy();
        }
        
        if (BulletProperties.HasFlag(BulletProperties.Chaining))
        {
            Debug.LogWarning("Chaining");
        }
        
        if (BulletProperties.HasFlag(BulletProperties.Forking))
        {
            Debug.LogWarning("Forking");
        }

        if (BulletProperties.HasFlag(BulletProperties.Piercing))
        {
            Debug.LogWarning("Piercing");
            _piercingCounter++;
            if (_piercingCounter > _piercingBulletData.AmountOfObjectsGoingThrough)
            {
                OnDestroy();
            }

            return;
        }

        OnDestroy();
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        transform.position += transform.up * Time.deltaTime * _baseBulletData.Speed;
    }

    private void AutoDestroy()
    {
        _autoDestroyTimer += Time.deltaTime;
        if (_baseBulletData.AutoDestroyTime <= _autoDestroyTimer)
        {
            OnDestroy();
        }
    }

    public void OnDestroy()
    {
        DeSpawn?.Invoke(Tag, gameObject);
    }
}

[Flags]
public enum BulletProperties
{
    None = 0,
    Piercing = 1,
    Chaining = 2,
    Forking = 4
}