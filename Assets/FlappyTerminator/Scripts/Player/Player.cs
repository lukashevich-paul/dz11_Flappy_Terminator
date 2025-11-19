using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMover))]
[RequireComponent(typeof(ScoreCounter))]
[RequireComponent(typeof(Collider2D))]
public class Player : MonoBehaviour
{
    private PlayerMover _playerMover;
    private ScoreCounter _scoreCounter;
    private Collider2D _collider2D;

    public event Action GameOver;

    private void Awake()
    {
        _playerMover = GetComponent<PlayerMover>();
        _scoreCounter = GetComponent<ScoreCounter>();
        _collider2D = GetComponent<Collider2D>();
        _collider2D.isTrigger = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<Damager>(out Damager damager))
        {
            GameOver?.Invoke();
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.TryGetComponent<Damager>(out _))
        {
            GameOver?.Invoke();
        }
    }
}
