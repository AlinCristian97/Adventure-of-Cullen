using UnityEngine;

[System.Serializable]
public class PlayerComponents : CharacterComponents
{
    [field: SerializeField] public AnyStateAnimator Animator { get; private set; }
}