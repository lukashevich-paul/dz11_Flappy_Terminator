using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Rocket : MonoBehaviour, IPoolMember<Rocket>
{
    [SerializeField] private float _speed = 4f;

    private Rigidbody2D _rigidbody2D;

    public event Action<Rocket> NeedReleaseItem;

    protected void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    protected void FixedUpdate()
    {
        Vector2 forvardVector = _rigidbody2D.transform.rotation * Vector2.left;
        _rigidbody2D.MovePosition(_rigidbody2D.position + forvardVector * _speed * Time.fixedDeltaTime);
    }

    protected void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.TryGetComponent<GameZone>(out _))
        {
            NeedReleaseItem?.Invoke(this);
        }

        if (collider.gameObject.TryGetComponent<PlayerRocket>(out _))
        {
            NeedReleaseItem?.Invoke(this);
        }

    }

    public void Initialize(Transform newTransform)
    {
        transform.position = newTransform.position;
        transform.rotation = newTransform.rotation;
    }
}
