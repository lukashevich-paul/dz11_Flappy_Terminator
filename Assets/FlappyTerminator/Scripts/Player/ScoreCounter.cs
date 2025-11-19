using System;
using UnityEngine;

[RequireComponent(typeof(PlayerMover))]
public class ScoreCounter : MonoBehaviour
{
    private int _score = 0;
    private int _lastMaximum = 20;
    private PlayerMover _playerMover;

    public event Action<int> ScoreChanged;

    public int Score => _score;
    public int Record => _lastMaximum;

    private void Awake()
    {
        _playerMover = GetComponent<PlayerMover>();
    }

    private void OnEnable()
    {
        _playerMover.Restart += Reset;
    }

    private void OnDisable()
    {
        _playerMover.Restart -= Reset;
    }

    public void Add()
    {
        _score++;

        if (_score > _lastMaximum)
        {
            _lastMaximum = _score;
        }

        ScoreChanged?.Invoke(_score);
    }

    public void Reset()
    {
        _score = 0;
        ScoreChanged?.Invoke(_score);
    }
}
