using UnityEngine;

public class WeaponComponent : MonoBehaviour
{
    public float weaponActiveTime;

    WeaponColliderController weaponCollider;

    float weaponActiveTimer;

    private void Start()
    {
        weaponCollider = GetComponentInChildren<WeaponColliderController>();

        if (weaponCollider != null)
        {
            weaponCollider.onWeaponCollisionEvent += OnWeaponCollision;
        }
    }

    private void Update()
    {
        if (weaponActiveTimer > 0)
        {
            weaponActiveTimer -= Time.deltaTime;
        }
    }

    public void SetWeaponActive()
    {
        weaponActiveTimer = weaponActiveTime;
    }

    public void OnWeaponCollision(IEventSource _source, IWeaponTarget _target)
    {
        if (weaponActiveTimer > 0)
        {
            TargetHitInfo hitInfo = new TargetHitInfo(_source);
            _target.OnTargetHit(hitInfo);
        }
    }

    private void OnDestroy()
    {
        if (weaponCollider != null)
        {
            weaponCollider.onWeaponCollisionEvent -= OnWeaponCollision;
        }
    }
}
