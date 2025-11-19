using UnityEngine;

public class EnemyRocketSpawnerPool : BasicSpawnerPool<Rocket>
{
    [SerializeField] private AudioSource _shoot;
    [SerializeField] private AudioSource _explosion;

    protected override void GetFromPool(Rocket item)
    {
        _shoot.PlayOneShot(_shoot.clip, 0.5f);
        base.GetFromPool(item);
    }

    protected override void ReleaseItem(Rocket item)
    {
        _explosion.PlayOneShot(_shoot.clip);
        base.ReleaseItem(item);
    }
}
