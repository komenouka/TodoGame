public class PlayerModel : UnitModel
{
    public int mp;
    public int maxMp;
    public int level = 1;
    public int exp = 0;

    public PlayerModel(string name, int hp, int mp) : base(name, hp)
    {
        this.maxMp = this.mp = mp;
    }
}