using UnityEngine;

[CreateAssetMenu(fileName = "CharacterAttributes", menuName = "Brushes/Actor/Attributes")]
public class CharacterAttributes : ScriptableObject {

    public Sprite DialogueSprite;
    public float InteractionRange;

    public Role Type;

    public string Name;

    public int MaxHealth;

    public int CurrentHealth;

    public int Damage;

    public float Speed;

    public float SprintSpeed;

    public GameObject Loot;

    public enum Role {
        Player,
        Enemy,
        PaintEnemy,
        NPC,
        Static
    }

    public CharacterAttributes Copy() {
        CharacterAttributes newCharacterAttributes = CreateInstance<CharacterAttributes>();
        
        newCharacterAttributes.DialogueSprite = DialogueSprite;
        newCharacterAttributes.InteractionRange = InteractionRange;
        newCharacterAttributes.Type = Type;
        newCharacterAttributes.Name = Name;
        newCharacterAttributes.MaxHealth = MaxHealth;
        newCharacterAttributes.CurrentHealth = CurrentHealth;
        newCharacterAttributes.Damage = Damage;
        newCharacterAttributes.Speed = Speed;
        newCharacterAttributes.Loot = Loot;
        newCharacterAttributes.SprintSpeed = SprintSpeed;

        return newCharacterAttributes;
    }
}