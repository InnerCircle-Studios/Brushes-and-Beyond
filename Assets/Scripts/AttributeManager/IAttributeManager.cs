public interface IAttributeManager {
    public CharacterAttributes GetAttributes();

    public bool IsAlive();
    public void ApplyDamage(int hp);

    public void ApplyHeal(int hp);
}