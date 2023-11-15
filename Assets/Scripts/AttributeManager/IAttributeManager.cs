public interface IAttrubuteManager
{
    public CharacterAttributes GetAttributes();

    public void ApplyDamage(int hp);

    public void ApplyHeal(int hp);
}