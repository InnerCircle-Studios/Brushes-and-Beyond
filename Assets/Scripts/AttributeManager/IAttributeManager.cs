public interface IAttributeManager {
    public CharacterData GetAttributes();
    public CharacterAttributes GetAttributeContainer();
    public void Setattributes(CharacterData attributes);
    public bool IsAlive();
    public void ApplyDamage(int hp);

    public void ApplyHeal(int hp);

    public void SetPaint(int paintLevel);
}