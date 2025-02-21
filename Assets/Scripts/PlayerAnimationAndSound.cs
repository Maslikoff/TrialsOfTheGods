using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator), typeof(AudioSource))]
public class PlayerAnimationAndSound : MonoBehaviour
{
    [Header("Animation Parameters")]
    [SerializeField] private string _moveParam = "IsMoving";
    [SerializeField] private string _jumpParam = "IsJumping";
    [SerializeField] private string _carryParam = "IsCarrying";

    [Header("Sound Effects")]
    [SerializeField] private AudioClip _runSound;
    [SerializeField] private AudioClip _jumpSound;
    [SerializeField] private AudioClip _pickUpSound;
    [SerializeField] private AudioClip _throwSound;

    private Animator _animator;
    private AudioSource _audioSource;
    private PlayerMovement _playerMovement;
    private PlayerCarry _playerCarry;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();

        _playerMovement = GetComponent<PlayerMovement>();
        _playerCarry = GetComponent<PlayerCarry>();
    }

    private void Update()
    {
        bool isMoving = Mathf.Abs(_playerMovement.GetMoveInput()) > 0.01f;
        _animator.SetBool(_moveParam, isMoving);

        if (isMoving && _playerMovement.IsGrounded())
        {
            if (!_audioSource.isPlaying || _audioSource.clip != _runSound)
            {
                _audioSource.clip = _runSound;
                _audioSource.Play();
            }
        }
        else
        {
            if (_audioSource.clip == _runSound && _audioSource.isPlaying)
                _audioSource.Stop();
        }

        _animator.SetBool(_jumpParam, !_playerMovement.IsGrounded());

        _animator.SetBool(_carryParam, _playerCarry.HasCarriedObject());
    }

    public void PlayJumpSound() => _audioSource.PlayOneShot(_jumpSound);

    public void PlayPickUpSound() => _audioSource.PlayOneShot(_pickUpSound);

    public void PlayThrowSound() => _audioSource.PlayOneShot(_throwSound);
}
