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
            photonView.RPC("RPC_ChangeActiveItem", RpcTarget.AllBuffered, fistsSO.label);
    }

    private void OnSlot2()
    {
        if (photonView.IsMine)
            photonView.RPC("RPC_ChangeActiveItem", RpcTarget.AllBuffered, pistolSO.label);
    }

    [PunRPC]
    private void RPC_ChangeActiveItem(string itemName)
    {
        // This switch is temporal because it should be replace of some loop which will search in whole inventory which item should be actually rendered
        switch (itemName)
        {
            case "Pistol":
                _activeItemSO = pistolSO;
                break;
            case "Fists":
                _activeItemSO = fistsSO;
                break;
            default: return;
        }

        _animationHandler.SwitchAnimationClip(_activeItemSO.idleAnimation, PlayerAnimationHandler.AnimationTypes.IDLE);
        _animationHandler.SwitchAnimationClip(_activeItemSO.walkingAnimation, PlayerAnimationHandler.AnimationTypes.WALKING);
        _animationHandler.SwitchAnimationClip(_activeItemSO.attachAnimation, PlayerAnimationHandler.AnimationTypes.ATTACK);

        Destroy(_activeItem);
        _activeItem = Instantiate(_activeItemSO.prefab, Vector3.zero, Quaternion.identity);

        _activeItem.transform.parent = rightForearm.transform;
        _activeItem.transform.SetLocalPositionAndRotation(_activeItemSO.prefab.transform.position, _activeItemSO.prefab.transform.rotation);
        _activeItem.transform.localScale = _activeItemSO.prefab.transform.localScale;
    }

    public ItemSO GetActiveItemSO()
    {
        return _activeItemSO;
    }

    public GameObject GetActiveItem()
    {
        return _activeItem;
    }
}
