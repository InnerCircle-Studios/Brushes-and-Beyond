public class AttributeManager : IAttributeManager {
    private CharacterAttributes attributes;

    public AttributeManager(CharacterAttributes attributes) {
        this.attributes = attributes.Copy();
    }

    public void Setattributes(CharacterData attributes) {
        this.attributes.CharData = attributes;
    }

    public CharacterData GetAttributes() {
        return attributes.CharData;
    }
    public CharacterAttributes GetAttributeContainer() {
        return attributes;
    }

    public bool IsAlive() {
        return attributes.CharData.CurrentHealth > 0;
    }

    public void ApplyDamage(int hp) {
        int newHealth = attributes.CharData.CurrentHealth - hp;
        if (newHealth >= 0) {
            attributes.CharData.CurrentHealth -= hp;
        }
        else {
            attributes.CharData.CurrentHealth = 0;
        }
    }

    public void ApplyHeal(int hp) {
        int newHealth = attributes.CharData.CurrentHealth + hp;
        if (newHealth <= attributes.CharData.MaxHealth) {
            attributes.CharData.CurrentHealth += hp;
        }
        else {
            attributes.CharData.CurrentHealth = attributes.CharData.MaxHealth;
        }
    }

    public void SetPaint(int paintLevel) {
        attributes.CharData.PaintCount = paintLevel;
        GameManager.Instance.GetWindowManager().UpdateTextWindow("PaintIndicator", paintLevel.ToString());
    }

}