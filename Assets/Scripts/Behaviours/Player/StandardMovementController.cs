using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class StandardMovementController : MonoBehaviour
{
    [SerializeField]
    private float speed;
    private Vector2 newVelocity;
    private Rigidbody2D rigidBody2D;

    public void OnMove(InputAction.CallbackContext context)
    {
        var direction = context.action.ReadValue<Vector2>();

        newVelocity = rigidBody2D.velocity;
        newVelocity.x = direction.x * speed;
        newVelocity.y = direction.y * speed;
        rigidBody2D.velocity = newVelocity;
    }

    private void Awake()
    {
        rigidBody2D = gameObject.GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }
}
