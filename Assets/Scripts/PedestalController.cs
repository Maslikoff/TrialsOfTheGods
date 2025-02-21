using UnityEngine;
using UnityEngine.Rendering.Universal;

public class PedestalController : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private GameObject _torchPrefab;
    [SerializeField] private Transform _plaseFire;

    [Header("UI")]
    [SerializeField] private GameObject _helpBarUI;

    [Header("Animations")]
    [SerializeField] private Animator _doorAnimator;
    [SerializeField] private string _openDoorTrigger = "Open";

    [Header("Sound")]
    [SerializeField] private AudioSource _doorAudioSource;

    private bool _hasTorch = false;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player")) 
            return;

        Transform torch = collision.transform.Find("CarryObject/Torch");
        if (torch == null || !torch.CompareTag("Torch")) 
            return;

        Light2D fire = torch.GetComponentInChildren<Light2D>();
        if (fire == null || !fire.enabled)
            return;

        _helpBarUI.SetActive(true);

        if (Input.GetKeyDown(KeyCode.F))
            PlaceTorchOnPedestal(torch.gameObject);
    }

    private void OnTriggerExit2D(Collider2D collision) => _helpBarUI.SetActive(false);

    private void PlaceTorchOnPedestal(GameObject torch)
    {
        _doorAudioSource.Play();

        if (_hasTorch) return;

        Destroy(torch);

        if (_torchPrefab != null)
        {
            GameObject newTorch = Instantiate(_torchPrefab, _plaseFire.position, _plaseFire.rotation);
            newTorch.transform.SetParent(_plaseFire);
        }

        _hasTorch = true;

        if (_doorAnimator != null)
            _doorAnimator.SetTrigger(_openDoorTrigger);
    }
}
