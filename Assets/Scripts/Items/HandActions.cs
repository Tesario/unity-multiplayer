using System.Collections;
using UnityEngine;

public class HandActions : ItemActions
{
    [SerializeField] private GameObject firePoint;
    [SerializeField] private ParticleSystem hitParticles;

    IEnumerator PerformDamageCoroutine()
    {
        yield return new WaitForSeconds(0.56f);
        hitParticles.Play();
    }

    public override void PrimaryAction()
    {
        StartCoroutine(PerformDamageCoroutine());
    }
}
