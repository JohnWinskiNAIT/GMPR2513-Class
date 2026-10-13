using System.Collections.Generic;
using UnityEngine;

public class GrenadeLauncher : MonoBehaviour
{
    [SerializeField] GameObject projectile, barrelEnd;
    [SerializeField] List<GameObject> grenades;
    [SerializeField] int maxAmmo;
    [SerializeField] float fireRate, fireVelocity, timeStamp;
    int index;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        for (int i = 0; i < maxAmmo; i++)
        {
            GameObject instantiatedObject = Instantiate(projectile);
            grenades.Add(instantiatedObject);
            instantiatedObject.SetActive(false);
        }        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Fire()
    {
        if (Time.time > fireRate + timeStamp)
        {
            bool found = false;
            for (int i = 0; i < grenades.Count && !found; i++)
            {
                if (grenades[i].activeSelf == false)
                {
                    grenades[i].SetActive(true);
                    
                    grenades[i].transform.position = barrelEnd.transform.position;
                    grenades[i].transform.rotation = barrelEnd.transform.rotation;
                    
                    grenades[i].GetComponent<Rigidbody>().linearVelocity = transform.forward * fireVelocity;
                    Physics.IgnoreCollision(gameObject.GetComponentInChildren<Collider>(), grenades[i].GetComponentInChildren<Collider>());

                    found = true;
                    timeStamp = Time.time;
                }
            }
        }
        
    }
}
