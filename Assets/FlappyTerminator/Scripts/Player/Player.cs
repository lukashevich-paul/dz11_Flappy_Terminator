using System;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Player : MonoBehaviour
{
    private Collider2D _collider2D;

    public event Action Died;

    private void Awake()
    {
        _collider2D = GetComponent<Collider2D>();
        _collider2D.isTrigger = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<Damager>(out Damager damager))
        {
            Died?.Invoke();
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.TryGetComponent<Damager>(out _))
        {
            Died?.Invoke();
        }
    }
}
