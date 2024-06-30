using Photon.Pun;
using UnityEngine;

public class CameraFollowPlayer : MonoBehaviourPun
{
    [SerializeField] private float smoothTime = 2f;
    [SerializeField] private float cameraFollowOffset = 0.5f;
    [SerializeField] private Transform player;

    private void Start()
    {
        if (!photonView.IsMine)
        {
            transform.gameObject.SetActive(false);
        }

        transform.parent = null;
    }

    void Update()
    {
        if (player == null)
            return;

        // Move camera outside of player to get rid of relative position/rotation
        var playerMovementInput = player.GetComponent<PlayerMovement>().GetPlayerMovementInput();

        var previousPosition = transform.position;
        var newPosition = new Vector3(
            player.transform.position.x + playerMovementInput.x * cameraFollowOffset * -1,
            player.transform.position.y + playerMovementInput.y * cameraFollowOffset * -1,
            transform.position.z);
        transform.position = Vector3.Slerp(previousPosition, newPosition, smoothTime * Time.deltaTime);
    }
}
