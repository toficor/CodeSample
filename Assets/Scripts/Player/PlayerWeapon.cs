using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon
{
    private PlayerWeaponData _playerWeaponData;
    private Transform _shootingPoint;

    private float _shootingTimer = 0f;

    public PlayerWeapon(Transform shootingPoint, PlayerWeaponData playerWeaponData)
    {
        this._shootingPoint = shootingPoint;
        this._playerWeaponData = playerWeaponData;
    }

    public void OnUpdate()
    {
        _shootingTimer += Time.deltaTime;

        if (PlayerInput.Shoot)
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        if (_shootingTimer <= _playerWeaponData.ShootingDelay)
        {
            return;
        }

        _shootingTimer = 0f;
        PoolingSystem.Instance.SpawnObject("Bullet", _shootingPoint.position, _shootingPoint.rotation);
    }
}