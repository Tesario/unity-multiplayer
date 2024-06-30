using UnityEngine;

public class DeactivateGameObjectAfterStart : MonoBehaviour
{
    private void Awake()
    {
        transform.gameObject.SetActive(false);
    }
}
