using System;
using UnityEngine;

public class PlayerRocket : Rocket, IPoolMember<PlayerRocket>
{
    public new event Action<PlayerRocket> NeedReleaseItem;

    protected new void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.TryGetComponent<GameZone>(out _))
        {
            NeedReleaseItem?.Invoke(this);
        }

        if (collider.gameObject.TryGetComponent<Damager>(out _))
        {
            NeedReleaseItem?.Invoke(this);
        }
    }
}
