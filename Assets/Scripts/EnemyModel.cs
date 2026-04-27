public class EnemyModel : UnitModel
{
    public int rewardExp;

    public EnemyModel(string name, int hp, int exp) : base(name, hp)
    {
        this.rewardExp = exp;
    }
}