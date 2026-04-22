using UnityEngine;

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
        hp = Mathf.Max(0, hp - finalDamage);
    }

    public void Heal(int amount)
    {
        hp = Mathf.Min(maxHp, hp + amount);
    }
}

public class PlayerStatus : UnitModel
{
    public int mp;
    public int maxMp;
    public int level;
    public int exp;

    public PlayerStatus(int hp, int mp) : base("プレイヤー", hp)
    {
        this.maxMp = this.mp = mp;
        this.level = 1;
        this.exp = 0;
    }
}

public class EnemyStatus : UnitModel
{
    public string enemyName;

    public EnemyStatus(string name, int hp) : base(name, hp)
    {
        this.enemyName = name;
    }
}