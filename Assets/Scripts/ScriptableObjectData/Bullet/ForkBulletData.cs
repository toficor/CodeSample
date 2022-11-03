using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ForkBulletData", menuName = "Data/Bullet/ForkBulletData", order = 1)]
public class ForkBulletData : BaseBulletData
{
    public int PartsAmount = 2;
    public float AngleOffset = 30f;

    public override void OnHit(Transform transform, Collision2D col,
        BulletCotroller.OnDestructDelegate onDestruct = null)
    {
        float facingRotation = Mathf.Atan2(transform.up.y, transform.up.x) * Mathf.Rad2Deg;
        float startingRotation = (facingRotation - AngleOffset / 2f) + 180f;
        float angleIncrease = AngleOffset / (PartsAmount - 1);

        for (int i = 0; i < PartsAmount; i++)
        {
            float tmpRot = startingRotation + angleIncrease * i;
            SpriteRenderer renderer = col.gameObject.GetComponent<SpriteRenderer>();
            GameObject bullet = PoolingSystem.Instance.SpawnObject("Bullet", renderer.bounds.center,
                Quaternion.Euler(0f, 0f, tmpRot + 90));
        }

        base.OnHit(transform, col, onDestruct);
    }
}