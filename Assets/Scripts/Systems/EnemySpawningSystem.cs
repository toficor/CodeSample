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

    //dorobic raycastowanie i sprawdzanie czy w danym miejscu mozna respic enemiesow
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