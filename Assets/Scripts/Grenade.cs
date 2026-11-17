using UnityEngine;

public class Grenade : MonoBehaviour
{
    [SerializeField] GameObject particlePrefab, explosionObject;
    ParticleSystem explosionParticle;

    AudioSource explosionSound;
    private void OnEnable()
    {
        Invoke("DeactivateGrenade", 5.0f);
    }

    private void DeactivateGrenade()
    {
        gameObject.SetActive(false);
    }

    private void Start()
    {
        explosionObject = Instantiate(particlePrefab);
        explosionParticle = explosionObject.GetComponent<ParticleSystem>(); 
        explosionSound = explosionObject.GetComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        IDamageable damageObject;
        if (collision.gameObject.TryGetComponent(out damageObject))
        {
            damageObject.Damage(5);
        }

        explosionObject.transform.position = transform.position;
        explosionObject.transform.rotation = Quaternion.FromToRotation(explosionObject.transform.forward, collision.contacts[0].normal);
        explosionParticle.Play();
        explosionSound.Play();
        //Physics.IgnoreLayerCollision(8, 9, true);

        DeactivateGrenade();
    }
}
