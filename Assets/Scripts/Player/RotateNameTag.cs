using Photon.Pun;
using TMPro;
using UnityEngine;

public class RotateNameTag : MonoBehaviourPunCallbacks
{
    private void Start()
    {
        GetComponent<TextMeshProUGUI>().text = photonView.Owner.NickName;
    }

    private void Update()
    {
        transform.rotation = Quaternion.Euler(0, 0, 0);
    }
}
