using UnityEngine;

[System.Serializable]
public class PlayerReferences
{
    [field: SerializeField] public AnyStateAnimator Animator { get; private set; }
    [field: SerializeField] public PlayerAnimations Animations { get; set; }
}