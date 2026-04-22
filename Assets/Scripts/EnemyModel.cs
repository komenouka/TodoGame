public class EnemyModel : UnitModel
{
    public EnemyModel(string name, int hp)
    {
        this.maxHp = hp;
        this.hp = hp;
    }
}