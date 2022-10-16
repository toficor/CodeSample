using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "BaseBulletData", menuName = "Data/Bullet/BaseBulletData", order = 1)]
public class BaseBulletData : ScriptableObject
{
    public float Speed = 5f;
    public float AutoDestroyTime = 5f;

    //public GameObject DestroyEffect;

}
