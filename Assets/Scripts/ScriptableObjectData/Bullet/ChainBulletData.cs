using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ChainBulletData", menuName = "Data/Bullet/ChainBulletData", order = 1)]
public class ChainBulletData : BaseBulletData
{
    public int JumpsAmount = 2;
    public float MaxRangeToNextJump = 5f;

    public override void OnHit(Transform transform, Collision2D col,
        BulletCotroller.OnDestructDelegate onDestruct = null)
    {
        base.OnHit(transform, col, onDestruct);
    }

    public void OnHit(Transform transform, Collision2D col, int currentChainJump,
        BulletCotroller.OnDestructDelegate onDestruct = null)
    {
        if (currentChainJump < JumpsAmount)
        {
            Transform nearestTarget =
                ShootingUtilities.GetNearestTarget(col.transform.position, MaxRangeToNextJump);

            if (!nearestTarget)
            {
                OnHit(transform, col, onDestruct);
                return;
            }

            Vector3 pos = nearestTarget.position - transform.position;
            float angle = Mathf.Atan2(pos.y, pos.x) * Mathf.Rad2Deg;

            SpriteRenderer renderer = col.gameObject.GetComponent<SpriteRenderer>();
            var bullet = PoolingSystem.Instance.SpawnObject("Bullet", renderer.bounds.center,
                Quaternion.AngleAxis(angle - 90, Vector3.forward));

            var bulletController = bullet.GetComponent<BulletCotroller>();
            bulletController.BulletProperties = BulletProperties.Chaining;
            bulletController.CurrentChainJump = currentChainJump + 1;
            OnHit(transform, col, onDestruct);
        }
    }
}