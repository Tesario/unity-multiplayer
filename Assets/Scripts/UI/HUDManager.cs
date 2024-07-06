using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HUDManager : MonoBehaviourPunCallbacks
{
    [SerializeField] private GameObject pauseScreen;
    [SerializeField] private GameObject respawnScreen;
    [SerializeField] private Image bloodScreen;

    public void Disconnect()
    {
        SceneManager.LoadScene("Lobby");
        PhotonNetwork.Disconnect();
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void ShowBloodScreen(float opacity)
    {
        var tempColor = bloodScreen.color;
        tempColor.a = opacity;
        bloodScreen.color = tempColor;
    }

    public void ShowRespawnScreen()
    {
        respawnScreen.SetActive(true);
        pauseScreen.SetActive(false);
    }

    private void OnPause()
    {
        if (!respawnScreen.activeSelf)
        {
            pauseScreen.SetActive(!pauseScreen.activeSelf);
        }
    }
}
