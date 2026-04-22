public class PlayerModel : UnitModel
{
    public int mp;
    public int maxMp;
    public int level;
    public int exp;

    public PlayerModel(int hp, int mp)
    {
        this.maxHp = hp;
        this.hp = hp;
        this.maxMp = mp;
        this.mp = mp;
        this.level = 1;
        this.exp = 0;
    }
}