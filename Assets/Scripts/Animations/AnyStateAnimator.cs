using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnyStateAnimator : MonoBehaviour
{
    private Animator _animator;
    private Dictionary<string, AnyStateAnimation> _animations = new Dictionary<string, AnyStateAnimation>();
    private string _currentAnimation = string.Empty;
    
    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void AddAnimations(params AnyStateAnimation[] animations)
    {
        foreach (AnyStateAnimation newAnimation in animations)
        {
            _animations.Add(newAnimation.Name, newAnimation);
        }
    }

    public void TryPlayAnimation(string newAnimation)
    {
        if (_currentAnimation == string.Empty)
        {
            _animations[newAnimation].Active = true;
        }
        else if (_currentAnimation != newAnimation)
        {
            _animations[_currentAnimation].Active = false;
            _animations[newAnimation].Active = true;
        }
        
        _currentAnimation = newAnimation;

        Animate();
    }

    private void Animate()
    {
        foreach (string key in _animations.Keys)
        {
            _animator.SetBool(key, _animations[key].Active);
        }
    }
}