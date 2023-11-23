using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private PacManWithGun _controls;
    private Vector2 direction;

    [SerializeField] private float playerSpeed = 3;

    // Start is called before the first frame update
    void Start()
    {
        _controls = new PacManWithGun();
        _controls.Player.Enable();

        _controls.Player.Move.performed += Move;
    }

    // Update is called once per frame
    void Update()
    {
        var velocity = new Vector3(direction.x, direction.y, 0f) * playerSpeed * Time.deltaTime;
        transform.position += velocity;
    }

    public void Move(InputAction.CallbackContext context)
    {
        var inVec = context.ReadValue<Vector2>();
        direction = inVec;
    }
}
