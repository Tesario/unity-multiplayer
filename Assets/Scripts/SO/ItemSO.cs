using UnityEngine;

[CreateAssetMenu(fileName = "ItemSO", menuName = "Scriptable Objects/ItemSO")]
public class ItemSO : ScriptableObject
{
    public string label;
    public AnimationClip walkingAnimation;
    public AnimationClip idleAnimation;
    public AnimationClip attachAnimation;
}
