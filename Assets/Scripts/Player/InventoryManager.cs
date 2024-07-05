using Photon.Pun;
using UnityEngine;

public class InventoryManager : MonoBehaviourPunCallbacks
{
    [SerializeField] private ItemSO fistsSO;
    [SerializeField] private ItemSO pistolSO;
    [SerializeField] private GameObject rightForearm;

    private ItemSO _activeItemSO;
    private GameObject _activeItem;
    private PlayerAnimationHandler _animationHandler;

    private void Awake()
    {
        _animationHandler = GetComponent<PlayerAnimationHandler>();
    }

    private void Start()
    {
        _activeItemSO = fistsSO;
        _activeItem = rightForearm.transform.GetChild(0).gameObject;
    }

    private void OnSlot1()
    {
        if (photonView.IsMine)
            ChangeActiveSlot(fistsSO);
    }

    private void OnSlot2()
    {
        if (photonView.IsMine)
            ChangeActiveSlot(pistolSO);
    }

    [PunRPC]
    private void ChangeActiveSlot(ItemSO newItem)
    {
        _activeItemSO = newItem;

        _animationHandler.SwitchAnimationClip(_activeItemSO.idleAnimation, PlayerAnimationHandler.AnimationTypes.IDLE);
        _animationHandler.SwitchAnimationClip(_activeItemSO.walkingAnimation, PlayerAnimationHandler.AnimationTypes.WALKING);
        _animationHandler.SwitchAnimationClip(_activeItemSO.attachAnimation, PlayerAnimationHandler.AnimationTypes.ATTACK);

        PhotonNetwork.Destroy(_activeItem);
        _activeItem = PhotonNetwork.InstantiateRoomObject(_activeItemSO.prefab.name, Vector3.zero, Quaternion.identity);

        _activeItem.transform.parent = rightForearm.transform;
        _activeItem.transform.SetLocalPositionAndRotation(_activeItemSO.prefab.transform.position, _activeItemSO.prefab.transform.rotation);
        _activeItem.transform.localScale = _activeItemSO.prefab.transform.localScale;
    }

    public ItemSO GetActiveItem()
    {
        return _activeItemSO;
    }
}
