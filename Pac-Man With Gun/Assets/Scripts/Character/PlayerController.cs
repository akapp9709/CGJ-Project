using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private PacManWithGun _controls;
    private Vector2 direction;
    private Rigidbody2D _rb;

    [SerializeField] private float playerSpeed = 3, jumpForce = 10;
    private bool _grounded;

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
    void Update()
    {
        // Ground Check
        _grounded = Physics2D.Raycast(transform.position, Vector2.down, 0.05f);

        // Velocity Manipulation
        var velocity = new Vector3(direction.x, direction.y, 0f) * playerSpeed * Time.deltaTime;
        transform.position += velocity;
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
        if (_grounded)
        {
            _rb.AddForce(Vector3.up * jumpForce);
        }
    }
}
