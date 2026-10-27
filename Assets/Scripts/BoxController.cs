using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxController : MonoBehaviour
{
    // Animator Variables
    Animator animator;

    // Health Script
    Health myHealth;
    HealthUI myUI;

    [SerializeField] float healthModifier = 20;

    [SerializeField] Health attackerHealth;

    [SerializeField] GameObject box, brokenBox, particles;

    // Start is called before the first frame update
    void Start()
    {
        myHealth = GetComponent<Health>();
        myUI = GetComponentInChildren<HealthUI>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CharacterDamaged(GameObject origin)
    {
        if (myHealth.GetCurrentHealth() <= 0 && animator.GetCurrentAnimatorStateInfo(0).IsName("Death") == false)
        {
            attackerHealth = origin.GetComponent<Health>();

            if (attackerHealth != null)
            {
                Debug.Log("heal ", attackerHealth.gameObject);
                attackerHealth.HealDamage(healthModifier, this.gameObject);
            }

            // Set the death animation to play.
            animator.SetTrigger("Death");

            // Switch boxes
            box.SetActive(false);
            brokenBox.SetActive(true);

            // Activate particles
            particles.SetActive(true);

            // Turn off colider and set physics to kinematic
            GetComponent<Collider>().enabled = false;
            GetComponent<Rigidbody>().isKinematic = true;

            // Turn off healthbar
            myUI.TurnOffUI();
        }
    }
}
