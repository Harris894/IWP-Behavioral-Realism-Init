using UnityEngine;

public delegate void OnWeaponCollision(IEventSource _source, IWeaponTarget _weaponTarget);

public class WeaponColliderController : MonoBehaviour
{
    public event OnWeaponCollision onWeaponCollisionEvent;
    IEventSource eventSource;

    private void Awake()
    {
        eventSource = GetComponentInParent<IEventSource>();
    }

    private void OnCollisionEnter(Collision _collision)
    {
        IWeaponTarget target = _collision.gameObject.GetComponent<IWeaponTarget>();
        if (target != null)
        {
            if (eventSource != null)
            {
                onWeaponCollisionEvent?.Invoke(eventSource, target);
            }
            else Debug.LogWarning("WeaponCollider hit a WeaponTarget, but there was no source - Collision will be ignored");
        }
    }
}












