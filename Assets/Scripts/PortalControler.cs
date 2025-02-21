using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class PortalControler : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _spritePortal;
    [SerializeField] private Light2D _portalLight;
    [SerializeField] private GameObject _helpBar;
    [SerializeField] private GameObject _colliderPortal;
    [SerializeField] private float _fadeDuration = 2f;
    [SerializeField] private float _maxLightIntensity = 9f;

    private AudioSource _portalSound;

    private bool _isPlayerTouching;
    private bool _isRockTouching;
    private bool _isActivated;

    private void Start()
    {
        _spritePortal = GetComponentInChildren<SpriteRenderer>();
        _portalLight = GetComponent<Light2D>();
        _portalSound = GetComponent<AudioSource>();


        Color initialColor = _spritePortal.color;
        initialColor.a = 0f;
        _spritePortal.color = initialColor;

        _portalLight.intensity = 0f;

        _spritePortal.transform.localScale = Vector2.zero;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _isPlayerTouching = true;
            if (!_isRockTouching)
               _helpBar.SetActive(true);
        }
        else if (collision.gameObject.CompareTag("Rock"))
        {
            _isRockTouching = true;
            if (!_isPlayerTouching)
                _helpBar.SetActive(true);
        }

        CheckActivation();
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            _isPlayerTouching = false;
        else if (collision.gameObject.CompareTag("Rock"))
            _isRockTouching = false;

        _helpBar.SetActive(false);
    }

    private void CheckActivation()
    {
        if (!_isActivated && _isPlayerTouching && _isRockTouching)
        {
            _isActivated = true;
            _helpBar.SetActive(false);
            StartCoroutine(ActivatePortal());
        }
    }

    private IEnumerator ActivatePortal()
    {
        _portalSound.Play();

        float elapsedTime = 0f;
        Color startColor = _spritePortal.color;
        Color targetColor = new Color(startColor.r, startColor.g, startColor.b, 1f);
        float startIntensity = _portalLight.intensity;

        Vector2 startScale = _spritePortal.transform.localScale;
        Vector2 targetScale = Vector2.one;

        while (elapsedTime < _fadeDuration)
        {
            float t = Mathf.Clamp01(elapsedTime / _fadeDuration);

            _spritePortal.color = Color.Lerp(startColor, targetColor, t);

            _portalLight.intensity = Mathf.Lerp(startIntensity, _maxLightIntensity, t);

            _spritePortal.transform.localScale = Vector2.Lerp(startScale, targetScale, t);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        _spritePortal.color = targetColor;
        _portalLight.intensity = _maxLightIntensity;
        _spritePortal.transform.localScale = targetScale;

        _colliderPortal.SetActive(true);
    }
}
