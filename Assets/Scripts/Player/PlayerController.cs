using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Transform _shootingPoint;
    [SerializeField] private PlayerWeaponData _playerWeaponData;

    private PlayerWeapon _playerWeapon;

    private void Awake()
    {
        Init();
    }

    private void Update()
    {
        _playerWeapon?.OnUpdate();
        Rotate();
    }

    private void Init()
    {
        _playerWeapon = new PlayerWeapon(_shootingPoint, _playerWeaponData);
    }

    private void Rotate()
    {
        Vector3 cursorPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        var position = transform.position;
        Vector2 dir = new Vector2(cursorPosition.x - position.x, cursorPosition.y - position.y);
        transform.up = dir;
    }
}