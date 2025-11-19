using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(EnemySpawnerPool))]
[RequireComponent(typeof(EnemyRocketSpawnerPool))]
public class EnemySpawn : MonoBehaviour
{
    [SerializeField] private float _radius;
    [SerializeField][Min(1)] private int _maxSpawnCount;
    [SerializeField] private float _spawnTime;

    private EnemySpawnerPool _enemySpawner;
    private EnemyRocketSpawnerPool _rocketSpawner;
    private Coroutine _coroutine;
    private WaitForSeconds _wait;

    private void Awake()
    {
        _enemySpawner = GetComponent<EnemySpawnerPool>();
        _rocketSpawner = GetComponent<EnemyRocketSpawnerPool>();
        _wait = new WaitForSeconds(_spawnTime);
    }

    private void Start()
    {
        _coroutine = StartCoroutine(SpawnEnemy());
    }

    private void OnEnable()
    {
        _enemySpawner.Shoot += GetRocket;
    }

    private void OnDisable()
    {
        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
        }

        _enemySpawner.Shoot -= GetRocket;
    }

    private void GetRocket(Transform newTransform)
    {
        _rocketSpawner.GetItem(newTransform);
    }

    private IEnumerator SpawnEnemy()
    {
        while (enabled)
        {
            for (int i = 0; i < Random.Range(1, _maxSpawnCount + 1); i++)
            {
                Transform newTransform = transform;
                Vector2 newPosition = newTransform.position;

                newPosition.y = Random.Range(-_radius, _radius);
                newTransform.position = newPosition;

                _enemySpawner.GetItem(newTransform);
            }

            yield return _wait;
        }

        if (_coroutine != null)
            StopCoroutine(_coroutine);

        yield return null;
    }
}
