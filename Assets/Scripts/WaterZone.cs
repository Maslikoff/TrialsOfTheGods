using UnityEngine;
using UnityEngine.Rendering.Universal;

public class WaterZone : MonoBehaviour
{
    private AudioSource _waterSoundSource;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        _waterSoundSource = GetComponent<AudioSource>();
        _waterSoundSource.Play();

        if (collision.CompareTag("Player"))
        {
            PlayerMovement playerMovement = collision.GetComponent<PlayerMovement>();
            if (playerMovement != null)
                playerMovement.moveSpeed -= playerMovement.moveSpeed / 2f;

            Light2D lightFire = collision.GetComponentInChildren<Light2D>();
            if (lightFire != null)
                lightFire.enabled = false;

            ParticleSystem effect = collision.GetComponentInChildren<ParticleSystem>();
            if(effect != null)
                effect.gameObject.SetActive(false);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerMovement playerMovement = collision.GetComponent<PlayerMovement>();
            playerMovement.moveSpeed += playerMovement.moveSpeed;
        }
    }
}
