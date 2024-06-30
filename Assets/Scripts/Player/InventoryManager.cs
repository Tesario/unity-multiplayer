using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    [SerializeField] private ItemSO fistsSO;
    [SerializeField] private ItemSO pistolSO;

    private ItemSO _activeItemSO;
    private PlayerAnimationHandler _animationHandler;

    private void Awake()
    {
        _animationHandler = GetComponent<PlayerAnimationHandler>();
    }

    private void Start()
    {
        _activeItemSO = fistsSO;
    }

    private void OnSlot1()
    {
        _activeItemSO = fistsSO;

        _animationHandler.SwitchAnimationClip(_activeItemSO.idleAnimation, PlayerAnimationHandler.AnimationTypes.IDLE);
        _animationHandler.SwitchAnimationClip(_activeItemSO.walkingAnimation, PlayerAnimationHandler.AnimationTypes.WALKING);
        _animationHandler.SwitchAnimationClip(_activeItemSO.attachAnimation, PlayerAnimationHandler.AnimationTypes.ATTACK);
    }

    private void OnSlot2()
    {
        _activeItemSO = pistolSO;

        _animationHandler.SwitchAnimationClip(_activeItemSO.idleAnimation, PlayerAnimationHandler.AnimationTypes.IDLE);
        _animationHandler.SwitchAnimationClip(_activeItemSO.walkingAnimation, PlayerAnimationHandler.AnimationTypes.WALKING);
        _animationHandler.SwitchAnimationClip(_activeItemSO.attachAnimation, PlayerAnimationHandler.AnimationTypes.ATTACK);
    }

    public ItemSO GetActiveItem()
    {
        return _activeItemSO;
    }
}
