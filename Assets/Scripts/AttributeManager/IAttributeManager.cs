public interface IAttributeManager {
    public CharacterData GetAttributes();
    public void Setattributes(CharacterData attributes);
    public bool IsAlive();
    public void ApplyDamage(int hp);

    public void ApplyHeal(int hp);
}