using Photon.Pun;
using UnityEngine;

public class VitalFunctionsManager : MonoBehaviourPunCallbacks, IDamageable
{
    [SerializeField] private ParticleSystem bloodParticle;
    [SerializeField] private int maxHealth = 100;
    [SerializeField] private HUDManager hudManager;
    [SerializeField] private GameObject body;

    private int _health;

    private void Start()
    {
        _health = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        photonView.RPC("RPC_SyncHealth", RpcTarget.AllBuffered, damage);
    }

    [PunRPC]
    private void RPC_SyncHealth(int damage)
    {
        _health -= damage;
        RecalculateBloodScreen();

        if (_health <= 0)
            Die();
    }

    private void Die()
    {
        body.SetActive(false);
        var particle = Instantiate(bloodParticle, transform.position, transform.rotation);
        particle.Play();
        Destroy(particle, 2f);

        hudManager.ShowRespawnScreen();
    }

    private void RecalculateBloodScreen()
    {
        var opacity = 1 - ((float)_health / maxHealth);
        if (opacity > 0.4f)
            hudManager.ShowBloodScreen(opacity);
    }
}
