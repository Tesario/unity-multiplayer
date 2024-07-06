using Photon.Pun;

public class HandlePrimaryAction : MonoBehaviourPunCallbacks
{
    private PlayerAnimationHandler _animationHandler;
    private InventoryManager _inventoryManager;

    private void Awake()
    {
        _animationHandler = GetComponent<PlayerAnimationHandler>();
        _inventoryManager = GetComponent<InventoryManager>();
    }

    public void OnPrimaryAction()
    {
        if (!photonView.IsMine)
        {
            return;
        }

        var currentAnimation = _animationHandler.GetCurrentAnimationInfo();
        if (currentAnimation.IsName("Attack"))
        {
            return;
        }

        photonView.RPC("RPC_SyncPrimaryAction", RpcTarget.All);
    }

    [PunRPC]
    private void RPC_SyncPrimaryAction()
    {
        _animationHandler.Attack();

        var activeItem = _inventoryManager.GetActiveItem();
        var itemActions = activeItem.GetComponent<ItemActions>();

        if (itemActions != null)
            itemActions.PrimaryAction();
    }
}
