using Photon.Pun;
using TMPro;
using UnityEngine;

public class LobbyManager : MonoBehaviourPunCallbacks
{
    [SerializeField] private TextMeshProUGUI createRoomInput;
    [SerializeField] private TextMeshProUGUI joinRoomInput;
    [SerializeField] private TextMeshProUGUI createRoomError;
    [SerializeField] private TextMeshProUGUI joinRoomError;

    public void ValidateCreateRoomInput()
    {
        if (createRoomInput.text.Length < 3)
        {
            createRoomError.text = "Room name must be greater than or equal to 3 characters";
            return;
        }
        createRoomError.text = "";
    }

    public void ValidateJoinRoomInput()
    {
        if (joinRoomInput.text.Length < 3)
        {
            joinRoomError.text = "Room name must be greater than or equal to 3 characters";
            return;
        }
        joinRoomError.text = "";
    }

    public void CreateRoom()
    {
        ValidateCreateRoomInput();
        PhotonNetwork.CreateRoom(createRoomInput.text);
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        createRoomError.text = message;
    }

    public void JoinRoom()
    {
        ValidateJoinRoomInput();
        PhotonNetwork.JoinRoom(joinRoomInput.text);
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        joinRoomError.text = message;
    }

    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel("GameScene");
    }
}
