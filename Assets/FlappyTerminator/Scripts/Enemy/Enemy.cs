using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(AudioSource))]
public class Enemy : MonoBehaviour, IPoolMember<Enemy>
{
    [SerializeField] private float _speed = 0f;
    [SerializeField][Min(1)] private int _maxRocketCount = 0;
    [SerializeField] private float _secondForShoot = 1f;
    [SerializeField] private Transform _bulletSpawner;

    private Rigidbody2D _rigidbody2D;
    private AudioSource _audioSource;
    private int _rocketCount = 0;
    private float _currentSpeed = 0;
    private float _minPich = 0.9f;
    private float _maxPich = 1.1f;
    private Coroutine _coroutine;

    public event Action<Enemy> NeedReleaseItem;
    public event Action<Transform> IsShoot;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _audioSource = GetComponent<AudioSource>();
    }

    private void FixedUpdate()
    {
        _rigidbody2D.velocity = Vector2.left * _currentSpeed;
    }

    public void Initialize(Transform newTransform)
    {
        transform.position = newTransform.position;
        _currentSpeed = _speed;
        _audioSource.pitch = Random.Range(_minPich, _maxPich);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.TryGetComponent<FireZone>(out _))
        {
            _rocketCount = Random.Range(1, _maxRocketCount + 1);
            Hang();

            _coroutine = StartCoroutine(Shoot());
        }

        if (collider.gameObject.TryGetComponent<PlayerRocket>(out _))
        {
            NeedReleaseItem?.Invoke(this);
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.TryGetComponent<GameZone>(out _))
        {
            NeedReleaseItem?.Invoke(this);
        }
    }

    private void Hang()
    {
        _currentSpeed = 0f;
    }

    private void FrontalAttack()
    {
        _currentSpeed = _speed;
    }

    private IEnumerator Shoot()
    {
        WaitForSeconds wait = new WaitForSeconds(_secondForShoot);

        while (gameObject.activeSelf && _rocketCount > 0)
        {
            _rocketCount--;
            IsShoot?.Invoke(_bulletSpawner.transform);
            yield return wait;
        }

        StopCoroutine(_coroutine);
        yield return null;
        FrontalAttack();
    }
}
