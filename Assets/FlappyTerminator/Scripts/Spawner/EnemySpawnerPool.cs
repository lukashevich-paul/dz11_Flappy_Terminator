using System;
using UnityEngine;

public class EnemySpawnerPool : BasicSpawnerPool<Enemy> 
{
    public event Action<Transform> Shoot;

    public override void GetItem(Transform newTransform)
    {
        Enemy item = Pool.Get();
        item.gameObject.SetActive(true);
        item.Initialize(newTransform);

        item.IsShoot += NeedShoot;
        item.NeedReleaseItem += Pool.Release;
    }

    protected override void ReleaseItem(Enemy item)
    {
        item.IsShoot -= NeedShoot;
        item.NeedReleaseItem -= Pool.Release;

        item.gameObject.SetActive(false);
    }

    private void NeedShoot(Transform newTransform)
    {
        Shoot?.Invoke(newTransform);
    }
}
