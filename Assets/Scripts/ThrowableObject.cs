using UnityEngine;

public class ThrowableObject : MonoBehaviour
{
    [SerializeField] private GameObject _helpBar;

    private PlayerCarry _playerCarry;

    private void Start()
    {
        _playerCarry = FindObjectOfType<PlayerCarry>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !_playerCarry.HasCarriedObject())
            _helpBar.SetActive(true);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && Input.GetKeyDown(KeyCode.F) && !_playerCarry.HasCarriedObject())
        {
            _playerCarry.PickUpObject(gameObject);
            _helpBar.SetActive(false);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            _helpBar.SetActive(false);
    }
}
