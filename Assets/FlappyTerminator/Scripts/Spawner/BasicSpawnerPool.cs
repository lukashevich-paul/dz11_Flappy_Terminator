using UnityEngine;
using UnityEngine.Pool;

public class BasicSpawnerPool<T> : MonoBehaviour where T : MonoBehaviour, IPoolMember<T>
{
    [SerializeField] protected T Prefab;
    [SerializeField] protected PlayerMover PlayerMover;

    protected ObjectPool<T> Pool;

    protected void Awake()
    {
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

    public virtual void GetItem(Transform newTransform)
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
        foreach (T item in GameObject.FindObjectsOfType<T>())
        {
            if (item.gameObject.activeInHierarchy)
                Pool.Release(item);
        }
    }
}
