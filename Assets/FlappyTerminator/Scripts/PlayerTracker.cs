using UnityEngine;

public class PlayerTracker : MonoBehaviour
{
    [SerializeField] private PlayerMover _playerMover;
    [SerializeField] private float _xOffset;

    private void Update()
    {
        Vector2 position = transform.position;
        position.x = _playerMover.transform.position.x + _xOffset;
        transform.position = position;
    }
}
