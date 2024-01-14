public class AttributeManager : IAttributeManager {
    private CharacterAttributes attributes;

    public AttributeManager(CharacterAttributes attributes) {
        this.attributes = attributes.Copy();
    }

    public void Setattributes(CharacterData attributes) {
        this.attributes.Attributes = attributes;
    }

    public CharacterData GetAttributes() {
        return attributes.Attributes;
    }

    public bool IsAlive() {
        return attributes.Attributes.CurrentHealth > 0;
    }

    public void ApplyDamage(int hp) {
        attributes.Attributes.CurrentHealth -= hp;
    }

    public void ApplyHeal(int hp) {
        attributes.Attributes.CurrentHealth += hp;
    }

    public void SetPaint(int paintLevel) {
        attributes.Attributes.PaintCount = paintLevel;
        GameManager.Instance.GetWindowManager().ShowTextWindow("PaintIndicator");
        GameManager.Instance.GetWindowManager().UpdateTextWindow("PaintIndicator", " "+paintLevel.ToString());
        if (paintLevel == 0) {
            GameManager.Instance.GetWindowManager().HideTextWindow("PaintIndicator");
        }

    }

}