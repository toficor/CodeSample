using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon
{
    private PlayerWeaponData _playerWeaponData;
    private Transform _shootingPoint;
    
    public PlayerWeapon(Transform shootingPoint, PlayerWeaponData playerWeaponData)
    {
        this._shootingPoint = shootingPoint;
        this._playerWeaponData = playerWeaponData;
    }

    public void OnUpdate()
    {
        
    }

    private void Shoot()
    {
        // instancjonowanie z poola
    }
}
