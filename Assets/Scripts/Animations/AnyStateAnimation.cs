using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AnyStateAnimation
{
    public string Name { get; private set; }
    public bool Active { get; private set; }

    public AnyStateAnimation(string name)
    {
        Name = name;
    }
}