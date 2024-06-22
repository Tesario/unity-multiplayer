using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerSpawner : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private GameInput gameInput;

    private float minX = -4f;
    private float maxX = 4f;
    private float minY = -4f;
    private float maxY = 4f;

    void Start()
    {
        Vector2 position = new Vector2(transform.position.x + Random.Range(minX, maxX), transform.position.y + Random.Range(minY, maxY));
        if (PhotonNetwork.IsConnected)
        {
            PhotonNetwork.Instantiate(player.name, position, Quaternion.identity);
        }
        else
        {
            Debug.LogWarning("Player is not connected to the server!");
            SceneManager.LoadScene("Lobby");
        }
    }
}
