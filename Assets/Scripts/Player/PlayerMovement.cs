using Photon.Pun;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviourPun
{
    [SerializeField] private float speed = 4f;
    [SerializeField] private float rotationSpeed = 720f;
    [SerializeField] private Transform rotatorControler;

    private PlayerAnimationHandler _animationHandler;
    private Rigidbody2D _rigidbody;
    private Vector2 _movementInput;
    private Vector2 _smoothedMovementInput;
    private Vector2 _movementInputSmoothVelocity;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _animationHandler = GetComponent<PlayerAnimationHandler>();
    }

    private void FixedUpdate()
    {
        if (photonView.IsMine)
        {
            SetPlayerVelocity();
            RotateInDirectionOfInput();
        }
    }

    private void SetPlayerVelocity()
    {
        _smoothedMovementInput = Vector2.SmoothDamp(_smoothedMovementInput, _movementInput, ref _movementInputSmoothVelocity, 0.1f);
        _rigidbody.MovePosition(_rigidbody.position + (_smoothedMovementInput * speed) * Time.fixedDeltaTime);

        if (_movementInput == Vector2.zero)
            _animationHandler.Idle();
        else
            _animationHandler.Walk();
    }

    private void RotateInDirectionOfInput()
    {
        if (_movementInput != Vector2.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(transform.forward, _smoothedMovementInput);
            Quaternion rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

            _rigidbody.MoveRotation(rotation);
        }
    }

    public void OnMovement(InputValue inputValue)
    {
        _movementInput = inputValue.Get<Vector2>();
    }

    public Vector2 GetPlayerMovementInput()
    {
        return _movementInput;
    }
}
