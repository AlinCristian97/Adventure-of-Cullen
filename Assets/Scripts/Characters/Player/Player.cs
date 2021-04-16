using System;
using StrategyPattern.Behaviours;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
    [field: SerializeField] public PlayerBrain Brain { get; private set; }
    [field: SerializeField] public PlayerActions Actions { get; private set; }
    [field: SerializeField] public PlayerStats Stats { get; private set; }
    [field: SerializeField] public PlayerComponents Components { get; private set; }
    [field: SerializeField] public PlayerReferences References { get; private set; }

    private void OnEnable()
    {
        // Brain.Input.Enable();
    }

    private void OnDisable()
    {
        // Brain.Input.Disable();
    }

    private void Awake()
    {
        Brain = new PlayerBrain(this);
        Actions = new PlayerActions(this);
    }

    private void Update()
    {
        Brain.HandleInput();
        Brain.CheckSurroundings();
    }

    private void FixedUpdate()
    {
        Actions.Move();
        Actions.HandleWallSlide();
    }
}