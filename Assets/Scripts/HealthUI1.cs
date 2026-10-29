using UnityEngine;
using UnityEngine.UI;

public class HealthUI1 : MonoBehaviour
{
    [SerializeField] GameObject healthbar;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        IDamageable damageObject;
        if (transform.parent.TryGetComponent(out damageObject))
        {
            // (max - curr) / max
            healthbar.transform.localScale =  new Vector3 ((float)damageObject.GetHealth()/damageObject.GetMaxHealth(),1,1);
        }
    }
}
