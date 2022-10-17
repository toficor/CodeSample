using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawningSystem : MonoBehaviour
{
    [SerializeField] private EnemySpawningSystemData _enemySpawningSystemData;

    private void Update()
    {
        if (PlayerInput.SpawnEnemyAtCursor)
        {
            SpawnAtCursorPosition();
        }

        if (PlayerInput.SpawnEnemyRandomly)
        {
            SpawnRandomly();
        }
    }

    //I was aware of case where enemies are spawning on each other, but in that case there are just returning to the pool. To prevent this, all you have to do is adding Physics2D.OverlapCircle like in the near enemy check (ShootingUtility.cs)
    private void SpawnRandomly()
    {
        for (int i = 0; i < _enemySpawningSystemData.Amoung; i++)
        {
            float x = Random.Range(0.1f, 0.9f);
            float y = Random.Range(0.1f, 0.9f);
            Vector2 pos = new Vector2(x, y);
            pos = Camera.main.ViewportToWorldPoint(pos);
            PoolingSystem.Instance.SpawnObject("Enemy", pos);
        }
    }

    private void SpawnAtCursorPosition()
    {
        Vector2 spawnPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        PoolingSystem.Instance.SpawnObject("Enemy", spawnPos);
    }
}