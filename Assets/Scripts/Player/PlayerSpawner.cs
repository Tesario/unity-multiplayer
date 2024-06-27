using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerSpawner : MonoBehaviourPunCallbacks
{
    [SerializeField] private GameObject playerPrefab;

    private float minX = -4f;
    private float maxX = 4f;
    private float minY = -4f;
    private float maxY = 4f;

    private void Start()
    {
        Vector3 position = new Vector3(transform.position.x + Random.Range(minX, maxX), transform.position.y + Random.Range(minY, maxY), playerPrefab.transform.localPosition.z);

        if (PhotonNetwork.IsConnected)
        {
            var player = PhotonNetwork.Instantiate(playerPrefab.name, position, Quaternion.identity);
        }
        else
        {
            Debug.LogWarning("Player is not connected to the server!");
            SceneManager.LoadScene("Lobby");
        }
    }
}
