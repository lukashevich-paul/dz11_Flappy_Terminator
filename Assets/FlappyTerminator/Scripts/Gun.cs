using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(InputHandler))]
public class Gun : MonoBehaviour
{
    [SerializeField] private Transform _bulletSpawner;

    private InputHandler _inputHandler;

    public event Action<Gun> IsShoot;

    public Transform BulletSpawner => _bulletSpawner.transform;

    private void Awake()
    {
        _inputHandler = GetComponent<InputHandler>();
    }

    private void OnEnable()
    {
        _inputHandler.Fire += Shoot;
    }
    private void OnDisable()
    {
        _inputHandler.Fire -= Shoot;
    }

    public void Shoot()
    {
        IsShoot?.Invoke(this);
    }
}
