using Photon.Pun;
using UnityEngine;

public class ConnectToServer : MonoBehaviourPunCallbacks
{
    [SerializeField] private GameObject lobby;
    [SerializeField] private GameObject loading;
    [SerializeField] private GameObject roomList;

    private void Awake()
    {
        lobby.SetActive(false);
    }

    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        loading.SetActive(false);
        lobby.SetActive(true);
    }
}
