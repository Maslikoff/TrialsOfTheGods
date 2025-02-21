using UnityEngine;

public class PlayerCarry : MonoBehaviour
{
    [Header("Carry Settings")]
    public Transform carryPosition;

    [Header("Throw Settings")]
    [SerializeField] private float _throwForce = 10f;
    [SerializeField] private float _massEffect = 1f;
    [SerializeField] private float _speedReductionMultiplier = 0.2f;

    private GameObject _carriedObject;
    private PlayerMovement _playerMovement;
    private PlayerAnimationAndSound _playerAnimationAndSound;

    public void Initialize(PlayerMovement playerMovement, PlayerAnimationAndSound playerAnimationAndSound)
    {
        _playerMovement = playerMovement;
        _playerAnimationAndSound = playerAnimationAndSound;
    }

    public void PickUpObject(GameObject objectToCarry)
    {
        if (_carriedObject != null) return;

        _carriedObject = objectToCarry;
        _carriedObject.transform.position = carryPosition.position;
        _carriedObject.transform.parent = carryPosition;

        Rigidbody2D objectRb = _carriedObject.GetComponent<Rigidbody2D>();
        if (objectRb != null)
        {
            // Changing the player's physical performance
            float speedReduction = objectRb.mass * _speedReductionMultiplier;
            _playerMovement.moveSpeed -= speedReduction;
            _playerMovement.jumpForce -= speedReduction;

            // Assigning an item
            objectRb.isKinematic = true;
            objectRb.velocity = Vector2.zero;
            objectRb.angularVelocity = 0f;
        }

        _playerAnimationAndSound.PlayPickUpSound();
    }
    
    public void ThrowObject()
    {
        if (_carriedObject == null) return;

        Rigidbody2D objectRb = _carriedObject.GetComponent<Rigidbody2D>();

        if (objectRb != null)
        {
            // The return of the player's previous physical performance
            float speedReduction = objectRb.mass * _speedReductionMultiplier;
            _playerMovement.moveSpeed += speedReduction;
            _playerMovement.jumpForce += speedReduction;

            // Returning values to an item
            objectRb.isKinematic = false;
            objectRb.transform.parent = null;
            Vector2 throwDirection = _playerMovement.IsFacingRight() ? Vector2.right : Vector2.left;
            objectRb.AddForce(throwDirection * _throwForce / (objectRb.mass * _massEffect), ForceMode2D.Impulse);
        }

        _playerAnimationAndSound.PlayThrowSound();

        _carriedObject = null;
    }

    // Checking if the player is holding an item
    public bool HasCarriedObject() => _carriedObject != null;

    // Getting the current item
    public GameObject GetCarriedObject() => _carriedObject;
}
