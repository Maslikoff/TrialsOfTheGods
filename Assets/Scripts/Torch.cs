using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Torch : MonoBehaviour
{
    public GameObject fire;

    private Collider2D _passThroughCollider; 
    private PlayerController _player; 

    private void Start()
    {
        _passThroughCollider = GetComponent<Collider2D>();
        _player = FindObjectOfType<PlayerController>();

        Collider2D playerCollider = _player.GetComponent<Collider2D>();
        Physics2D.IgnoreCollision(playerCollider, _passThroughCollider, true);
    }
}
