using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerWeaponData", menuName = "Data/Player/PlayerWeaponData", order = 1)]
public class PlayerWeaponData : ScriptableObject
{
    public float ShootingDelay = 0.2f;
}
