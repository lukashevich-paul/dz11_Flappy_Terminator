using System.Collections;
using UnityEngine;

public class PlayerRocketSpawner : BasicSpawnerPool<PlayerRocket>
{
    [SerializeField] private Gun _gun;
    [SerializeField] private float _recharge = 0.5f;
    [SerializeField] private AudioSource _shoot;
    [SerializeField] private AudioSource _explosion;

    private Coroutine _coroutine;
    private WaitForSeconds _wait;

    private void Start()
    {
        _wait = new WaitForSeconds(_recharge);
    }

    protected new void OnEnable()
    {
        base.OnEnable();
        _gun.IsShoot += Shoot;
    }

    protected new void OnDisable()
    {
        base.OnDisable();
        _gun.IsShoot -= Shoot;
    }

    private void Shoot(Gun gun)
    {
        if (_coroutine == null)
            _coroutine = StartCoroutine(Recharge(gun));
    }

    private IEnumerator Recharge(Gun gun)
    {
        PlayerRocket rocket = Pool.Get();
        rocket.NeedReleaseItem += Pool.Release;
        rocket.Initialize(gun.BulletSpawner);

        yield return _wait;

        StopCoroutine(_coroutine);
        _coroutine = null;
    }

    protected override void GetFromPool(PlayerRocket item)
    {
        _shoot.PlayOneShot(_shoot.clip, 0.5f);
        base.GetFromPool(item);
    }

    protected override void ReleaseItem(PlayerRocket item)
    {
        _explosion.PlayOneShot(_shoot.clip);
        item.NeedReleaseItem -= Pool.Release;

        base.ReleaseItem(item);
    }
}
