using System;
public class Health : IHealth
{
    public int CurrentHealth { get; private set; }

    private int MaxHealth;

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

    public void Heal(int healAmount)
    {
        CurrentHealth = Math.Min(CurrentHealth + healAmount, MaxHealth);
    }

    public void Set(int healthAmount)
    {
        CurrentHealth = healthAmount;
    }
}
