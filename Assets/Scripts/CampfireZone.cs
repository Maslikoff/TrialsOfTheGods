using UnityEngine;
using UnityEngine.Rendering.Universal;

public class CampfireZone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Light2D lightFire = collision.GetComponentInChildren<Light2D>();
            if (lightFire != null)
                lightFire.enabled = true;

            ParticleSystem effect = collision.GetComponentInChildren<ParticleSystem>();
            if (effect != null)
                effect.gameObject.SetActive(true);

            Torch torch = collision.GetComponentInChildren<Torch>();
            if (torch != null)
                torch.fire.SetActive(true);
        }
    }
}
