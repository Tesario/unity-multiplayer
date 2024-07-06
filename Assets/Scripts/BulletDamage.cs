using UnityEngine;

public class BulletDamage : MonoBehaviour
{
    [SerializeField] private int damage = 15;

    void OnTriggerEnter2D(Collider2D collision)
    {
        var damageable = collision.GetComponent<IDamageable>();
        damageable?.TakeDamage(damage);
        Destroy(transform.gameObject);
    }
}
