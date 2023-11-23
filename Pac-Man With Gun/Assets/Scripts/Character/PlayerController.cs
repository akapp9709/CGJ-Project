using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private PacManWithGun _controls;
    private Vector2 direction;
    private Rigidbody2D _rb;

    [SerializeField] private float playerSpeed = 3, jumpForce = 10, inAirSpeed = 1.5f, groundCheckDistance = 0.1f;
    [SerializeField] private float gravity = 10;
    public bool _grounded;
    public LayerMask groundCheckLayer;

    [SerializeField] private float coyoteTime = 0.2f;
    private float coyoteCounter;

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, transform.position + Vector3.down * groundCheckDistance);
    }

    // Start is called before the first frame update
    void Start()
    {
        //New Input System Generated Class
        _controls = new PacManWithGun();
        //Jump handled with Rigidbody
        _rb = GetComponent<Rigidbody2D>();

        //Control delegate mapping
        _controls.Player.Enable();

        _controls.Player.Move.performed += Move;
        _controls.Player.Jump.started += Jump;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // Ground Check
        _grounded = Physics2D.Raycast(transform.position, Vector2.down, groundCheckDistance, groundCheckLayer);

        var currentSpeed = _grounded ? playerSpeed : inAirSpeed;

        if (_grounded)
        {
            coyoteCounter = coyoteTime;
        }
        else
        {
            coyoteCounter -= Time.deltaTime;
        }

        // Velocity Manipulation
        var velocity = new Vector3(direction.x, direction.y, 0f) * currentSpeed * Time.deltaTime;

        transform.position += velocity;

        if (coyoteCounter < 0)
        {
            _rb.AddForce(Vector3.down * gravity);
        }
    }

    //Movement Method
    public void Move(InputAction.CallbackContext context)
    {
        //Gets vector from player input
        var inVec = context.ReadValue<Vector2>();
        // Allocates x-direction of input
        direction = new Vector2(inVec.x, 0f);
    }

    //Jump Method
    public void Jump(InputAction.CallbackContext context)
    {
        // only runs if player is grounded
        if (coyoteCounter >= 0)
        {
            _rb.AddForce(Vector3.up * jumpForce);
        }
    }
}
