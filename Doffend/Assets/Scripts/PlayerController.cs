using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(EntityController2D), typeof(SpriteRenderer))]
public class PlayerController : MonoBehaviour
{
    public float pixelsPerMeter = 16;
    public float movementSpeed = 32;

    public float jumpHeight = 2.0f;
    public float timeToJumpApex = 0.3f;

    public float joyStickDeadZone = 0.1f;

    public int MaxNumInAirJumps = 0;
    private int _numInAirJumps = 0;

    private float _accelerationTimeAirborne = .2f;
    private float _accelerationTimeGrounded = .1f;
    private enum MoveDirection
    {
        mdToRight,
        mdToLeft
    }

    private EntityController2D _controller;
    private SpriteRenderer _spriteRenderer;

    private MoveDirection _lastMoveDirection = MoveDirection.mdToRight;


    private float _gravity = -20;
    private Vector3 _velocity;
    private float _jumpVelocity;

    // Variables used in update to update the player state based on input
    private Vector2 _playerInputMovement;
    private bool _playerShouldJump = false;

    private float _velocityXSmoothingState;


    // Start is called before the first frame update
    void Start()
    {
        _controller = GetComponent<EntityController2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();

        _recalculateCoefficients();
    }

    /// <summary>
    /// Called by PlayerInput when the move event occurs.
    /// </summary>
    public void OnMove(InputValue value)
    {
        Vector2 incomingValue = value.Get<Vector2>();

        float deltaX = Mathf.Abs(incomingValue.x);
        float deltaY = Mathf.Abs(incomingValue.y);

        if (deltaY > deltaX && Mathf.Abs(incomingValue.x) < joyStickDeadZone)
            incomingValue.x = 0.0f;
        if (deltaX > deltaY && Mathf.Abs(incomingValue.y) < joyStickDeadZone)
            incomingValue.y = 0.0f;

        _playerInputMovement = incomingValue;
    }

    /// <summary>
    /// Called by PlayerInput when the jump event occurs.
    /// </summary>
    public void OnJump(InputValue value)
    {
        _playerShouldJump = value.Get<float>() > 0.2f;
    }

    // Update is called once per frame
    void Update()
    {
        // If we're standing on ground, gravity shouldn't be applied.
        bool shouldResetYVelocity = false;

        if ((_controller.collisions & (EntityController2D.CollisionDirection.cdTop
                                       | EntityController2D.CollisionDirection.cdBottom)) != 0)
            shouldResetYVelocity = true;

        if (shouldResetYVelocity)
            _velocity.y = 0.0f;

        float targetXVelocity = _playerInputMovement.x * movementSpeed;
        float accelaration = _controller.collisions.HasFlag(EntityController2D.CollisionDirection.cdBottom) ? _accelerationTimeGrounded : _accelerationTimeAirborne;
        _velocity.x = Mathf.SmoothDamp(_velocity.x, targetXVelocity, ref _velocityXSmoothingState, accelaration);
        // If we received a jump event since the last update, we perform a jump if possible
        if (_playerShouldJump && _canJump())
        {
            _velocity.y = _jumpVelocity;
            _playerShouldJump = false;
        }

        // Apply gravity to the player
        _velocity.y += _gravity * Time.deltaTime;

        // Perform the move, which checks for collions.
        _controller.move(_velocity * Time.deltaTime);

        // Update the sprite orientation.
        if (_playerInputMovement.x > 0.5f)
            _lastMoveDirection = MoveDirection.mdToRight;
        else if (_playerInputMovement.x < -0.5f)
            _lastMoveDirection = MoveDirection.mdToLeft;
        _spriteRenderer.flipX = _lastMoveDirection == MoveDirection.mdToLeft;


    }

    bool _canJump()
    {
        bool isGrounded = _controller.collisions.HasFlag(EntityController2D.CollisionDirection.cdBottom);

        if (isGrounded)
        {
            _numInAirJumps = 0;
            return true;
        }

        if (_numInAirJumps < MaxNumInAirJumps)
        {
            _numInAirJumps += 1;
            return true;
        }

        return false;
    }

    void _recalculateCoefficients()
    {
        _gravity = -(2 * jumpHeight) / Mathf.Pow(timeToJumpApex, 2);
        _jumpVelocity = Mathf.Abs(_gravity) * timeToJumpApex;
    }
}
