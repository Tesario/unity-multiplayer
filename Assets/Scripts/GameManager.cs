using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviourPunCallbacks
{
    [SerializeField] private GameObject pauseWindow;

    public void Disconnect()
    {
        PhotonNetwork.Disconnect();
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        SceneManager.LoadScene("Lobby");
    }

    public void Exit()
    {
        Application.Quit();
    }

    private void OnPause(InputValue inputValue)
    {
        if (inputValue.isPressed)
        {
            if (pauseWindow.activeSelf)
            {
                pauseWindow.SetActive(false);
            }
            else
            {
                pauseWindow.SetActive(true);
            }
        }
    }
}
