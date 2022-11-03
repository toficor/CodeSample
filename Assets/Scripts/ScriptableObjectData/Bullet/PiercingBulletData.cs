using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PiercingBulletData", menuName = "Data/Bullet/PiercingBulletData", order = 1)]
public class PiercingBulletData : BaseBulletData
{
    public int AmountOfObjectsGoingThrough = 3;

    public override void OnHit(Transform transform, Collision2D col,
        BulletCotroller.OnDestructDelegate onDestruct = null)
    {
        base.OnHit(transform, col, onDestruct);
    }

    public void OnHit(Transform transform, Collision2D col, int piercingCounter,
        BulletCotroller.OnDestructDelegate onDestruct = null)
    {
        if (piercingCounter < AmountOfObjectsGoingThrough)
        {
            return;
        }

        OnHit(transform, col, onDestruct);
    }
}