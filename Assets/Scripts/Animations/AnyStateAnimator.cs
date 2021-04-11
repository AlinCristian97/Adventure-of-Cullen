using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnyStateAnimator : MonoBehaviour
{
    private Animator _animator;
    private Dictionary<string, AnyStateAnimation> _animations = new Dictionary<string, AnyStateAnimation>();
    private string _currentAnimation;
    
    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void AddAnimations(params AnyStateAnimation[] animations)
    {
        foreach (AnyStateAnimation anim in animations)
        {
            _animations.Add(anim.Name, anim);
        }
    }   
}