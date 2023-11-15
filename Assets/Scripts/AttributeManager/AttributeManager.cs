public class AttributeManager : IAttrubuteManager
{
    public AttributeManager()
    {
        _Attrubutes = new CharacterAttributes();
    }

    public CharacterAttributes GetAttributes()
    {
        return _Attrubutes;
    }

    public void ApplyDamage(int hp)
    {
        _Attrubutes._CurrentHealth -= hp;
    }

    public void ApplyHeal(int hp)
    {
        _Attrubutes._CurrentHealth += hp;
    }

    private CharacterAttributes _Attrubutes;
}