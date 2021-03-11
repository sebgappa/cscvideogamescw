using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class GenericMovementControl : MonoBehaviour
{
    [SerializeField]
    private float speed;
    private Vector2 velocity;
    private Rigidbody2D rigidBody2D;

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

    void Move(float xDirection, float yDirection)
    {
        velocity = rigidBody2D.velocity;
        velocity.x = xDirection * speed; 
        velocity.x = yDirection * speed; 
    }


}
