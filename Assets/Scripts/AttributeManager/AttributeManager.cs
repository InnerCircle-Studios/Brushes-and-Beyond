public class AttributeManager : IAttrubuteManager {
    private CharacterAttributes attributes;

    public AttributeManager(CharacterAttributes attributes) {
        this.attributes = attributes.Copy();
    }

    public CharacterAttributes GetAttributes() {
        return attributes;
    }

    public bool IsAlive(){
        return attributes.CurrentHealth > 0;
    }

    public void ApplyDamage(int hp) {
        attributes.CurrentHealth -= hp;
    }

    public void ApplyHeal(int hp) {
        attributes.CurrentHealth += hp;
    }

}