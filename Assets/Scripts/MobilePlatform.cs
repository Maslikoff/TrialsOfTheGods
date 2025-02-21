using UnityEngine;

public class MobilePlatform : MonoBehaviour
{
    [SerializeField] private Transform[] _waypoints;
    [SerializeField] private float _speed = 2f;
    [SerializeField] private float _waitTime = 1f;

    private int _currentWaypointIndex = 0;
    private float _waitCounter = 0f;
    private bool _isWaiting = false;

    private Vector2 _previousPosition;
    private Vector2 _currentVelocity;

    private Rigidbody2D _playerRb;
    private bool _isPlayerOnPlatform = false;

    private void Start()
    {
        _previousPosition = transform.position;
    }

    private void FixedUpdate()
    {
        MovingPlatform();
    }

    private void MovingPlatform()
    {
        if (_isWaiting)
        {
            _waitCounter += Time.deltaTime;
            if (_waitCounter >= _waitTime)
            {
                _isWaiting = false;
                _waitCounter = 0f;
            }
            return;
        }

        Transform targetWaypoint = _waypoints[_currentWaypointIndex];
        transform.position = Vector2.MoveTowards(transform.position, targetWaypoint.position, _speed * Time.deltaTime);

        if (Vector2.Distance(transform.position, targetWaypoint.position) < 0.1f)
        {
            _currentWaypointIndex = (_currentWaypointIndex + 1) % _waypoints.Length;
            _isWaiting = true;
        }

        _currentVelocity = (Vector2)transform.position - _previousPosition;
        _previousPosition = transform.position;

        if (_isPlayerOnPlatform && _playerRb != null)
            _playerRb.MovePosition(_playerRb.position + _currentVelocity);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (collision.contacts[0].normal.y < -0.5f)
            {
                _playerRb = collision.gameObject.GetComponent<Rigidbody2D>();
                if (_playerRb != null)
                    _isPlayerOnPlatform = true;
            }
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Rigidbody2D playerRb = collision.gameObject.GetComponent<Rigidbody2D>();
            if (playerRb != null)
                playerRb.velocity += _currentVelocity;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _isPlayerOnPlatform = false;
            _playerRb = null;
        }
    }
}
