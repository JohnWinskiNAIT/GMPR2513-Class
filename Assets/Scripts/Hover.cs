using UnityEngine;

//[RequireComponent(typeof(Rigidbody))]
public class Hover : MonoBehaviour
{
    RaycastHit hit;
    [SerializeField] Rigidbody rb;

    [SerializeField] float maxDistance, forceAmount;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponentInParent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    { 
        if (Physics.Raycast(transform.position, -transform.up, out hit, maxDistance))
        {
            if (rb != null)
            {
                float distance = Vector3.Distance(transform.position, hit.point);

                //rb.AddForce(Vector3.up * forceAmount * ((maxDistance - distance) / maxDistance));
                rb.AddForceAtPosition(Vector3.up * forceAmount * ((maxDistance - distance) / maxDistance), transform.position);

                rb.linearDamping = 5;
                rb.angularDamping = 5;
            }            
        }
        else
        {
            if (rb != null)
            {
                rb.linearDamping = 0;
                rb.angularDamping = 0;
            }
        }
    }
}
