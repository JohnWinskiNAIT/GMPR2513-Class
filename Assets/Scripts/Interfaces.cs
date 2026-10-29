using UnityEngine;

public interface IInteractable
{ 
    void Interact();
}

public interface IDamageable
{
    void Damage(int damage);
    void Heal(int damage);

    int GetHealth();
    int GetMaxHealth();
}