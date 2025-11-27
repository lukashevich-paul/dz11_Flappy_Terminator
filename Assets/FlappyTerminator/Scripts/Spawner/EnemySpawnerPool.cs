using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawnerPool : BasicSpawnerPool<Enemy>
{
    [SerializeField] private float _radius;
    [SerializeField][Min(1)] private int _maxSpawnCount;
    [SerializeField] private float _spawnTime;
    [SerializeField] private EnemyRocketSpawnerPool _enemyRocketSpawnerPool;

    private Coroutine _coroutine;
    private WaitForSeconds _wait;

    private void Start()
    {
        _wait = new WaitForSeconds(_spawnTime);

        _coroutine = StartCoroutine(SpawnEnemy());
    }

    private new void OnDisable()
    {
        base.OnDisable();

        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
        }
    }

    public override void Spawn(Transform newTransform)
    {
        Enemy enemy = Pool.Get();
        enemy.gameObject.SetActive(true);
        enemy.InitializeEnemy(newTransform, _enemyRocketSpawnerPool);

        enemy.NeedReleaseItem += Pool.Release;
    }

    protected override void ReleaseItem(Enemy enemy)
    {
        enemy.NeedReleaseItem -= Pool.Release;

        enemy.gameObject.SetActive(false);
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

                Spawn(newTransform);
            }

            yield return _wait;
        }

        if (_coroutine != null)
            StopCoroutine(_coroutine);

        yield return null;
    }
}
