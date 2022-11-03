using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.U2D;

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
    public int CurrentChainJump { get; set; } = 0;

    public delegate void OnDestructDelegate();

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
            EffectLoadingSystem.Instance.TryGetData(BulletProperties.None.ToString())
                ?.OnHit(this.transform, col, OnDestruct);
        }

        if (BulletProperties.HasFlag(BulletProperties.Chaining))
        {
            ChainBulletData chainingBulletData =
                (ChainBulletData)EffectLoadingSystem.Instance.TryGetData(BulletProperties.Chaining.ToString());

            if (!chainingBulletData)
            {
                Debug.LogError("chaining Bullet data is equal null");
                return;
            }
            
            chainingBulletData.OnHit(this.transform, col, CurrentChainJump, OnDestruct);
            Debug.Log("Chaining");
        }

        if (BulletProperties.HasFlag(BulletProperties.Piercing))
        {
            Debug.Log("Piercing");
            _piercingCounter++;
            PiercingBulletData piercingBulletData =
                (PiercingBulletData)EffectLoadingSystem.Instance.TryGetData(BulletProperties.Piercing.ToString());

            if (!piercingBulletData)
            {
                Debug.LogError("chaining Bullet data is equal null");
                return;
            }
            
            piercingBulletData.OnHit(this.transform, col, _piercingCounter, OnDestruct);
        }

        if (BulletProperties.HasFlag(BulletProperties.Forking))
        {
            ForkBulletData forkBulletData =
                (ForkBulletData)EffectLoadingSystem.Instance.TryGetData(BulletProperties.Forking.ToString());
            forkBulletData.OnHit(this.transform, col, OnDestruct);
            
            if (!forkBulletData)
            {
                Debug.LogError("forking bullet data is equal null");
                return;
            }
            
            Debug.Log("Forking");
        }
    }

    private void Update()
    {
        Move();
        AutoDestroy();
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
            OnDestruct();
        }
    }

    public void OnDestruct()
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