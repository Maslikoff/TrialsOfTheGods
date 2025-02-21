using UnityEngine;

[RequireComponent(typeof(PlayerMovement) ,typeof(PlayerCarry), typeof(PlayerAnimationAndSound))]
public class PlayerController : MonoBehaviour
{
    private PlayerMovement _playerMovement;
    private PlayerCarry _playerCarry;
    private PlayerAnimationAndSound _playerAnimationAndSound;

    private void Start()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();

        _playerMovement = GetComponent<PlayerMovement>();
        _playerMovement.Initialize(rb);

        _playerCarry = GetComponent<PlayerCarry>();
        _playerAnimationAndSound = GetComponent<PlayerAnimationAndSound>();

        _playerCarry.Initialize(_playerMovement, _playerAnimationAndSound);
    }

    private void Update()
    {
        float moveInput = Input.GetAxis("Horizontal");
        _playerMovement.Move(moveInput);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            _playerAnimationAndSound.PlayJumpSound();
            _playerMovement.Jump();
        }

        if (Input.GetKeyDown(KeyCode.G))
            _playerCarry.ThrowObject();
    }

    private void OnCollisionEnter2D(Collision2D collision) => _playerMovement.OnCollisionEnter2D(collision);

    private void OnCollisionExit2D(Collision2D collision) => _playerMovement.OnCollisionExit2D(collision);
}