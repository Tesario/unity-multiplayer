using Photon.Pun;
using UnityEngine;

public class CameraFollow : MonoBehaviourPun
{
    [SerializeField] private Vector3 offset = new Vector3(0f, 0f, -10f);
    [SerializeField] private float smoothTime = 2f;
    [SerializeField] private float cameraFollowOffset = 0.5f;
    private Vector3 velocity = Vector3.zero;
    [SerializeField] private Transform player;

    private Vector2 _movementInputSmoothVelocity;

    private void Start()
    {
        if (!photonView.IsMine)
        {
            transform.gameObject.SetActive(false);
        }
    }

    void Update()
    {
        // Move camera outside of player to get rid of relative position/rotation
        transform.parent = null;

        var playerMovementInput = player.GetComponent<PlayerMovement>().IsMoving();

        var previousPosition = transform.position;
        var newPosition = new Vector3(
            player.transform.position.x + playerMovementInput.x * cameraFollowOffset * -1,
            player.transform.position.y + playerMovementInput.y * cameraFollowOffset * -1,
            transform.position.z);
        transform.position = Vector3.Slerp(previousPosition, newPosition, smoothTime * Time.deltaTime);
    }
}
