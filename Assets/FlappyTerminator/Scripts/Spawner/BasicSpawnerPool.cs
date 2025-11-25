using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class BasicSpawnerPool<T> : MonoBehaviour where T : MonoBehaviour, IPoolMember<T>
{
    [SerializeField] protected T Prefab;
    [SerializeField] protected PlayerMover PlayerMover;

    protected ObjectPool<T> Pool;
    protected List<T> _activeItems;

    protected void Awake()
    {
        _activeItems = new List<T>();

        Pool = new ObjectPool<T>(
            createFunc: InstantiateItem,
            actionOnGet: GetFromPool,
            actionOnRelease: ReleaseItem,
            actionOnDestroy: DestroyItem
        );
    }

    protected void OnEnable()
    {
        PlayerMover.Restart += ResetPool;
    }

    protected void OnDisable()
    {
        PlayerMover.Restart -= ResetPool;
    }

    public virtual void Spawn(Transform newTransform)
    {
        T item = Pool.Get();
        item.gameObject.SetActive(true);
        item.Initialize(newTransform);

        item.NeedReleaseItem += Pool.Release;
    }

    protected virtual T InstantiateItem()
    {
        T item = Instantiate(Prefab);

        return item;
    }

    protected virtual void GetFromPool(T item)
    {
        item.gameObject.SetActive(true);
        _activeItems.Add(item);
    }

    protected virtual void ReleaseItem(T item)
    {
        item.NeedReleaseItem -= Pool.Release;
        item.gameObject.SetActive(false);
    }

    protected virtual void DestroyItem(T item)
    {
        Destroy(item.gameObject);
    }

    protected virtual void ResetPool()
    {
        for (int i = _activeItems.Count - 1; i > 0; i--)
        {
            T item = _activeItems[i];

            if (item.gameObject.activeSelf)
            {
                Pool.Release(item);
                _activeItems.Remove(item);
            }
        }
    }
}
