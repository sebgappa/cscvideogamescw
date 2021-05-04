using UnityEngine;
using UnityEngine.InputSystem;

public interface IPlayer
{
    Rigidbody2D Rigidbody2D { get; }
    BoxCollider2D BoxCollider2D { get; }
    PlayerInput PlayerInput { get; }
    Score Score { get; }
    int MaxHealth { get; }
    int CurrentHealth { get; set; }
    Animator Animator { get; }
}
