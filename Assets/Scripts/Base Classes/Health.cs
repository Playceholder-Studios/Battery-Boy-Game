using System;
using UnityEngine;
public class Health : IHealth
{
    public int CurrentHealth { get; private set; }

    public int MaxHealth { get; private set; }

    private const int DefaultMaxHealth = 1;

    public Health() : this(DefaultMaxHealth) { }

    public Health(int maxHealth)
    {
        MaxHealth = maxHealth;
        CurrentHealth = MaxHealth;
    }

    public void Damage(int damageAmount)
    {
        CurrentHealth -= damageAmount;
    }

    /// <summary>
    /// Increases the health by <paramref name="healAmount"/>.
    /// </summary>
    /// <param name="healAmount"></param>
    public void Heal(int healAmount)
    {
        CurrentHealth = Math.Min(CurrentHealth + healAmount, MaxHealth);
    }

    public void Set(int healthAmount)
    {
        CurrentHealth = healthAmount;
    }
}
