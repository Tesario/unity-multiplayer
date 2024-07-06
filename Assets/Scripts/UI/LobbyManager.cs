using Photon.Pun;
using Photon.Realtime;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LobbyManager : MonoBehaviourPunCallbacks
{
    [SerializeField] private TextMeshProUGUI createRoomInput;
    [SerializeField] private TextMeshProUGUI createRoomError;
    [SerializeField] private GameObject roomsGrid;
    [SerializeField] private GameObject roomGridItem;
    [SerializeField] private TextMeshProUGUI nicknameInput;
    [SerializeField] private TextMeshProUGUI nicknameError;

    private bool isCreateRoomInputValid = false;
    private bool isNicknameInputValid = false;

    public void ValidateCreateRoomInput()
    {
        if (createRoomInput.text.Length < 3)
        {
            createRoomError.text = "Room name must be greater than or equal to 3 characters";
            isCreateRoomInputValid = false;
            return;
        }
        createRoomError.text = "";
        isCreateRoomInputValid = true;
    }

    public void ValidateNicknameInput()
    {
        if (nicknameInput.text.Length < 3)
        {
            nicknameError.text = "Nickname must be greater than or equal to 3 characters";
            isNicknameInputValid = false;
            return;
        }
        nicknameError.text = "";
        isNicknameInputValid = true;
    }

    public void CreateRoom()
    {
        if (PhotonNetwork.NetworkClientState != ClientState.JoinedLobby)
            return;

        ValidateCreateRoomInput();
        ValidateNicknameInput();

        if (!isCreateRoomInputValid || !isNicknameInputValid)
            return;

        PhotonNetwork.LocalPlayer.NickName = nicknameInput.text;

        PhotonNetwork.CreateRoom(createRoomInput.text, new() { IsVisible = true });
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        createRoomError.text = message;
    }

    public void JoinRoom(string roomName)
    {
        if (PhotonNetwork.NetworkClientState != ClientState.JoinedLobby)
            return;

        ValidateNicknameInput();

        if (!isNicknameInputValid)
            return;

        PhotonNetwork.LocalPlayer.NickName = nicknameInput.text;

        PhotonNetwork.JoinRoom(roomName);
    }

    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel("GameScene");
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        foreach (Transform item in roomsGrid.transform)
        {
            Destroy(item.gameObject);
        }

        foreach (RoomInfo roomInfo in roomList)
        {
            if (roomInfo.RemovedFromList)
            {
                continue;
            }

            var prefab = Instantiate(roomGridItem, roomsGrid.transform);

            var roomNameText = prefab.transform.Find("Room Name").GetComponent<TextMeshProUGUI>();
            roomNameText.text = roomInfo.Name;

            var playersText = prefab.transform.Find("Players").GetComponent<TextMeshProUGUI>();
            playersText.text = $"{roomInfo.PlayerCount} out of {roomInfo.MaxPlayers} Players";

            var joinButton = prefab.transform.Find("Join Button").GetComponent<Button>();
            joinButton.onClick.AddListener(() => JoinRoom(roomInfo.Name));
        }
    }
}
