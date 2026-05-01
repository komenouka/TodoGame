using System;

public abstract class UnitModel
{
    public string name;
    public int hp;
    public int maxHp;
    public bool isGuarding;
    public bool IsDead => hp <= 0;

    public UnitModel(string name, int hp)
    {
        this.name = name;
        this.maxHp = this.hp = hp;
    }

    public virtual void Damage(int amount)
    {
        int finalDamage = isGuarding ? amount / 2 : amount;
        hp = Math.Max(0, hp - finalDamage);
    }

    public void Heal(int amount)
    {
        hp = Math.Min(maxHp, hp + amount);
    }
}