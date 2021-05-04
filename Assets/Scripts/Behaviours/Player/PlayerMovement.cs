using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CommandDispatcher))]
public class PlayerMovement : MonoBehaviour, IPlayerMovement
{
    public Animator Animator { get; set; }
    public Rigidbody2D Rigidbody2D { get; set; }
    public SpriteRenderer SpriteRenderer { get; set; }

    private CommandDispatcher _commandDispatcher;

    public void OnMove(InputAction.CallbackContext context)
    {
        _commandDispatcher.DispatchCommand(new MoveCommand(this, context.action.ReadValue<Vector2>()));
    }

    private void Awake()
    {
        _commandDispatcher = gameObject.GetComponent<CommandDispatcher>();
        Rigidbody2D = gameObject.GetComponent<Rigidbody2D>();
        SpriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        Animator = gameObject.GetComponent<Animator>();
    }
}
