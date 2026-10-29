using UnityEngine;

public class AnimatedCylinder : MonoBehaviour, IInteractable, IDamageable
{
    Animator animator;
    [SerializeField] int currentHealth, maxHealth = 100;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Interact()
    {
        Debug.Log("Make me animate");
        animator.SetTrigger("Jump");
    }

    public void Damage(int damage)
    {
        currentHealth -= damage;
    }

    public void Heal(int damage)
    {
        currentHealth += damage;
    }

    public int GetHealth()
    {
        return currentHealth;
    }

    public int GetMaxHealth()
    {
        return maxHealth;
    }
}
