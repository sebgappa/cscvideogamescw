using UnityEngine;

public interface IPlayerMovement
{
    Animator Animator { get; }
    Rigidbody2D Rigidbody2D { get; }
    SpriteRenderer SpriteRenderer { get; }
}
