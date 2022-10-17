using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ChainBulletData", menuName = "Data/Bullet/ChainBulletData", order = 1)]
public class ChainBulletData : ScriptableObject
{
    public int JumpsAmount = 2;
    public float MaxRangeToNextJump = 5f;
}
