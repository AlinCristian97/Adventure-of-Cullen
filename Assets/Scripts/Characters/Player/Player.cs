using StrategyPattern.Behaviours;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
    [field: SerializeField] public PlayerBrain Brain { get; private set; }
    public PlayerActions Actions { get; private set; }
    [field: SerializeField] public PlayerStats Stats { get; private set; }
    [field: SerializeField] public PlayerComponents Components { get; private set; }
    [field: SerializeField] public PlayerReferences References { get; private set; }

    private void OnEnable()
    {
        Brain.Input.Enable();
    }

    private void OnDisable()
    {
        Brain.Input.Disable();
    }

    private void Awake()
    {
        Brain = new PlayerBrain(this);
        Actions = new PlayerActions(this, new HumanJump());
    }

    private void Start()
    {
        AddAnimations();
    }
    
    private void Update()
    {
        Brain.HandleDecisions();
    }

    private void FixedUpdate()
    {
        Actions.Move();
    }
    
    private void AddAnimations()
    {
        AnyStateAnimation[] animations = {
            new AnyStateAnimation("Idle"),
            new AnyStateAnimation("Run"),
            new AnyStateAnimation("Jump")
        };
        
        References.Animator.AddAnimations(animations);
    }
}