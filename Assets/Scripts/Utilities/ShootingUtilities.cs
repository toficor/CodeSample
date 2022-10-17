using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;

public static class ShootingUtilities
{
    public static Transform GetNearestTarget(Vector2 center, float radius)
    {
        int maxTargets = 5;
        Collider2D[] results = new Collider2D[maxTargets];

        int layerMask =~ LayerMask.GetMask("Bullet");
        int size = Physics2D.OverlapCircleNonAlloc(center, radius, results,layerMask);
        if (size <= 0)
        {
            return null;
        }

        Transform nearestTarget = results[0].transform;
        float distance = Vector2.Distance(center, nearestTarget.position);

        for (int i = 0; i < size; i++)
        {
            float tmpDist = Vector2.Distance(center, results[i].transform.position);

            if (tmpDist < distance)
            {
                nearestTarget = results[i].transform;
            }
        }

        return nearestTarget;
    }
}