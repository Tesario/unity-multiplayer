using UnityEngine;

public class PistolActions : ItemActions
{
    [SerializeField] private GameObject firePoint;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float bulletSpeed = 20f;
    [SerializeField] private ParticleSystem fireParticles;

    public override void PrimaryAction()
    {
        var bullet = Instantiate(bulletPrefab, firePoint.transform.position, firePoint.transform.rotation);
        bullet.GetComponent<Rigidbody2D>().AddForce(firePoint.transform.right * bulletSpeed);
        fireParticles.Play();
        Destroy(bullet, 5f);
    }
}
