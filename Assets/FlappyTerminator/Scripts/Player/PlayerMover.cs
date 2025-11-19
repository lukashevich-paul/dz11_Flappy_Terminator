using System;
using UnityEngine;

[RequireComponent(typeof(InputHandler))]
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMover : MonoBehaviour
{
    [SerializeField] private float _tapForce;
    [SerializeField] private float _rotationSpeed;
    [SerializeField] private float _maxRotationZ;
    [SerializeField] private float _minRotationZ;

    private InputHandler _inputHandler;
    private Vector3 _startPosition;
    private Rigidbody2D _rigidbody2D;
    private Quaternion _maxRotation;
    private Quaternion _minRotation;

    public event Action Restart;

    private void Awake()
    {
        _inputHandler = GetComponent<InputHandler>();

        _startPosition = transform.position;
        _rigidbody2D = GetComponent<Rigidbody2D>();

        _maxRotation = Quaternion.Euler(0, 0, _maxRotationZ);
        _minRotation = Quaternion.Euler(0, 0, _minRotationZ);
    }

    private void OnEnable()
    {
        _inputHandler.Fly += Fly;
        _inputHandler.Reset += Reset;
    }

    private void OnDisable()
    {
        _inputHandler.Fly -= Fly;
        _inputHandler.Reset -= Reset;
    }

    private void Fly()
    {
        _rigidbody2D.velocity = new Vector2(0f, _tapForce);
        transform.rotation = _maxRotation;
    }

    private void Update()
    {
        transform.rotation = Quaternion.Lerp(transform.rotation, _minRotation, _rotationSpeed * Time.deltaTime);
    }

    public void Reset()
    {
        Restart?.Invoke();

        transform.position = _startPosition;
        transform.rotation = Quaternion.identity;
        _rigidbody2D.velocity = Vector2.zero;
    }
}
