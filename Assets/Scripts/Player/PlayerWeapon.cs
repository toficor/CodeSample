using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon
{
    private PlayerWeaponData _playerWeaponData;
    private Transform _shootingPoint;

    private float _shootingTimer = 0f;
    private BulletProperties _currentBulletEffects = BulletProperties.None;

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

        ManageBulletEffects();
    }

    private void ManageBulletEffects()
    {
        if (PlayerInput.EnablePiercing)
        {
            _currentBulletEffects ^= BulletProperties.Piercing;
            Debug.Log(_currentBulletEffects);
        }

        if (PlayerInput.EnableForking)
        {
            _currentBulletEffects ^= BulletProperties.Forking;

            Debug.Log(_currentBulletEffects);
        }

        if (PlayerInput.EnableChaining)
        {
            _currentBulletEffects ^= BulletProperties.Chaining;

            Debug.Log(_currentBulletEffects);
        }
    }

    private void Shoot()
    {
        if (_shootingTimer <= _playerWeaponData.ShootingDelay)
        {
            return;
        }

        _shootingTimer = 0f;
        var bullet = PoolingSystem.Instance.SpawnObject("Bullet", _shootingPoint.position, _shootingPoint.rotation);
        var bulletController = bullet.GetComponent<BulletCotroller>();

        if (!bulletController)
        {
            Debug.LogError("Trying to shoot gameobject without bullet controller");
            bullet.SetActive(false);
            return;
        }

        bulletController.BulletProperties = _currentBulletEffects;
    }
}