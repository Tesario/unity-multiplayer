using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

public class VitalFunctionsManager : MonoBehaviourPunCallbacks, IDamageable
{
    [SerializeField] private Image bloodScreen;
    [SerializeField] private int maxHealth = 100;

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
        if (!photonView.IsMine)
            return;

        _health -= damage;
        RecalculateBloodScreen();
    }

    private void RecalculateBloodScreen()
    {
        var opacity = 1 - ((float)_health / maxHealth);

        if (opacity > 0.2f)
        {
            var tempColor = bloodScreen.color;
            tempColor.a = opacity;
            bloodScreen.color = tempColor;
        }
    }
}
