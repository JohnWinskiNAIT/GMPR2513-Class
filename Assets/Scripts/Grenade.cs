using UnityEngine;

public class Grenade : MonoBehaviour
{
    private void OnEnable()
    {
        Invoke("DeactivateGrenade", 5.0f);
    }

    private void DeactivateGrenade()
    {
        gameObject.SetActive(false);
    }

    private void OnCollisionEnter(Collision collision)
    {
        IDamageable damageObject;
        if (collision.gameObject.TryGetComponent(out damageObject))
        {
            damageObject.Damage(5);
        }
        DeactivateGrenade();
    }
}
