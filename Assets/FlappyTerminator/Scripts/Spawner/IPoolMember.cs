using System;
using UnityEngine;

public interface IPoolMember<T>
{
    public event Action<T> NeedReleaseItem;

    public void Initialize(Transform newTransform);
}
