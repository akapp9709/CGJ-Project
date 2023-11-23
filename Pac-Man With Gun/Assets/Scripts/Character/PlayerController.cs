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
    public bool _grounded;

    // Start is called before the first frame update
    void Start()
    {
        _controls = new PacManWithGun();
        _rb = GetComponent<Rigidbody2D>();
        _controls.Player.Enable();

        _controls.Player.Move.performed += Move;
        _controls.Player.Jump.started += Jump;
    }

    // Update is called once per frame
    void Update()
    {
        _grounded = Physics2D.Raycast(transform.position, Vector2.down, 0.05f);
        var velocity = new Vector3(direction.x, direction.y, 0f) * playerSpeed * Time.deltaTime;
        transform.position += velocity;
    }

    public void Move(InputAction.CallbackContext context)
    {
        var inVec = context.ReadValue<Vector2>();
        direction = new Vector2(inVec.x, 0f);
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (_grounded)
        {
            _rb.AddForce(Vector3.up * jumpForce);
        }
    }
}
