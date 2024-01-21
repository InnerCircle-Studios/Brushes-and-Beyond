using UnityEngine;

public class Hostile : Actor {
    public override void Start() {
        _Player = FindAnyObjectByType<Player>();

        _HostileStateMachine = new HostileStateMachine(this, _Player, _isMelee, _colour.ToString());
    }

    public override void Update() {
        _HostileStateMachine._Colour = _colour.ToString();

        _HostileStateMachine.GetCurrentState().UpdateState();
        if (!GetAttrubuteManager().IsAlive()) {
            OnDeath();
        }
    }

    public override void HandleMeleeAttack() {
        foreach (Actor hits in GetCombat().MeleeAttack(GetRigidBody().position, _AttributeManager.GetAttributes().AttackRange, "Player")) {
            hits.GetAttrubuteManager().ApplyDamage(GetAttrubuteManager().GetAttributes().Damage);
            PlayRandomAttackSound();
            StartCoroutine(FlashSpriteOnHit(hits.GetComponent<SpriteRenderer>()));

        }
    }

    public void PlayRandomAttackSound() {
        int random = Random.Range(0, 5);
        switch (random) {
            case 0:
                AudioManager.instance.PlaySfx("HitEffect1");
                break;
            case 1:
                AudioManager.instance.PlaySfx("HitEffect2");
                break;
            case 2:
                AudioManager.instance.PlaySfx("HitEffect3");
                break;
            case 3:
                AudioManager.instance.PlaySfx("HitEffect4");
                break;
            case 4:
                AudioManager.instance.PlaySfx("HitEffect5");
                break;
        }
    }

    public override void HandleRangedAttack() {
        Instantiate(_projectile, transform.position, Quaternion.identity);
    }

    public Player GetPlayer() {
        return _Player;
    }

    private void OnDeath() {
        _HostileStateMachine._isDead.Value = true;
    }

    private bool GetHostileType() {
        return _isMelee;
    }


    private HostileStateMachine _HostileStateMachine;

    private Player _Player;
    [SerializeField] private bool _isMelee = false;
    [SerializeField] private Colour _colour;
    [SerializeField] private GameObject _projectile;


    public enum Colour {
        Red,
        Yellow,
        Blue,
    }
}