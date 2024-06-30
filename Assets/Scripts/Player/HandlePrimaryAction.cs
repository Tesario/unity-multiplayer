using UnityEngine;
using UnityEngine.InputSystem;

public class HandlePrimaryAction : MonoBehaviour
{
    private PlayerAnimationHandler _animationHandler;

    private void Awake()
    {
        _animationHandler = GetComponent<PlayerAnimationHandler>();
    }

    public void OnPrimaryAction(InputValue inputValue)
    {
        _animationHandler.Attack();
    }
}
