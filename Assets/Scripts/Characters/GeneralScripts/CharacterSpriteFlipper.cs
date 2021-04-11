using UnityEngine;

public class CharacterSpriteFlipper : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;
    [SerializeField] private Player _player;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (_player.Stats.Direction.x != 0)
        {
            _spriteRenderer.flipX = _player.Stats.Direction.x < 0;
        }
    }
}
