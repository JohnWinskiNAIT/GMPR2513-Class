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
        DeactivateGrenade();
    }
}
