using UnityEngine;

public class PlayerAnimationHandler : MonoBehaviour
{
    [SerializeField] private Animator animator;

    private AnimatorOverrideController _animatorOverrideController;

    public enum AnimationTypes
    {
        IDLE,
        WALKING,
        ATTACK
    }

    private string _activeIdleAnimationName = "Idle";
    private string _activeWalkingAnimationName = "Walking";
    private string _activeAttackAnimationName = "Attack";

    public void Start()
    {
        _animatorOverrideController = new AnimatorOverrideController(animator.runtimeAnimatorController);
        animator.runtimeAnimatorController = _animatorOverrideController;
    }

    public void SwitchAnimationClip(AnimationClip clip, AnimationTypes animationType)
    {
        switch (animationType)
        {
            case AnimationTypes.IDLE:
                _animatorOverrideController[_activeIdleAnimationName] = clip;
                _activeIdleAnimationName = clip.name;
                break;
            case AnimationTypes.WALKING:
                _animatorOverrideController[_activeWalkingAnimationName] = clip;
                _activeWalkingAnimationName = clip.name;
                break;
            case AnimationTypes.ATTACK:
                _animatorOverrideController[_activeAttackAnimationName] = clip;
                _activeAttackAnimationName = clip.name;
                break;
        }
    }

    public void Idle()
    {
        animator.SetBool("IsWalking", false);
    }

    public void Walk()
    {
        animator.SetBool("IsWalking", true);
    }

    public void Attack()
    {
        animator.SetTrigger("Attack");
    }
}
