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

    public event Action<string, GameObject> DeSpawn;

    private void OnEnable()
    {
        _piercingCounter = 0;
        _autoDestroyTimer = 0;
    }

    //while I was writing this logic, I had the feeling that there is some "nicer way" to write this.
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (BulletProperties.Equals(BulletProperties.None))
        {
            Debug.Log("None");
            OnDestroy();
        }

        if (BulletProperties.HasFlag(BulletProperties.Chaining))
        {
            if (CurrentChainJump < _chainBulletData.JumpsAmount)
            {
                Transform nearestTarget =
                    ShootingUtilities.GetNearestTarget(col.transform.position, _chainBulletData.MaxRangeToNextJump);

                if (!nearestTarget)
                {
                    OnDestroy();
                    return;
                }

                Vector3 pos = nearestTarget.position - transform.position;
                float angle = Mathf.Atan2(pos.y, pos.x) * Mathf.Rad2Deg;

                SpriteRenderer renderer = col.gameObject.GetComponent<SpriteRenderer>();
                var bullet = PoolingSystem.Instance.SpawnObject("Bullet", renderer.bounds.center,
                    Quaternion.AngleAxis(angle - 90, Vector3.forward));

                var bulletController = bullet.GetComponent<BulletCotroller>();
                bulletController.BulletProperties = BulletProperties.Chaining;
                bulletController.CurrentChainJump = CurrentChainJump + 1;

                Debug.Log("Chaining");
            }
        }

        if (BulletProperties.HasFlag(BulletProperties.Forking))
        {
            float facingRotation = Mathf.Atan2(transform.up.y, transform.up.x) * Mathf.Rad2Deg;
            float startingRotation = (facingRotation - _forkBulletData.AngleOffset / 2f) + 180f;
            float angleIncrease = _forkBulletData.AngleOffset / (_forkBulletData.PartsAmount - 1);

            for (int i = 0; i < _forkBulletData.PartsAmount; i++)
            {
                float tmpRot = startingRotation + angleIncrease * i;
                SpriteRenderer renderer = col.gameObject.GetComponent<SpriteRenderer>();
                GameObject bullet = PoolingSystem.Instance.SpawnObject("Bullet", renderer.bounds.center,
                    Quaternion.Euler(0f, 0f, tmpRot + 90));
            }

            Debug.Log("Forking");
        }

        if (BulletProperties.HasFlag(BulletProperties.Piercing))
        {
            Debug.Log("Piercing");
            _piercingCounter++;
            if (_piercingCounter < _piercingBulletData.AmountOfObjectsGoingThrough)
            {
                return;
            }
        }

        OnDestroy();
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