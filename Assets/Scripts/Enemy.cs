using UnityEngine;
using EventSys;

public enum EnemyType {
    Default,
    Small,
    Medium,
    Large
}

public sealed class Enemy : MonoBehaviour {
    public SpriteRenderer SR           = null;
    public EnemyType      EnemyType    = EnemyType.Default;
    public int            Health       = 100;
    public int            AttackDamage = 10;
    public float          MoveSpeed    = 1f;
    public GameObject     Explosion    = null;
    public Vector3 MoveDirection = new Vector3(0, 0, 1);
    public float ZOffset = 0;

    const float DESTROY_TIME = 1f;
    bool _isDead     = false;
    int  _initHealth = 0;
    bool _isMoving   = false;

    void Start() {
        SR = GetComponentInChildren<SpriteRenderer>();
        _initHealth = Health;
        _isMoving = true;
    }

    void Update() {
        if (_isMoving && !_isDead) {
            transform.Translate(MoveDirection * MoveSpeed * Time.deltaTime);
        }
    }

    public void DoDamage(int amount) {
        Health = Mathf.Clamp(Health - amount, 0, _initHealth);
        if (Health == 0 && !_isDead) {
            Die();
        }
    }

    public void StopMoving() {
        _isMoving = false;
        Die();
    }

    void Die() {
		if ( _isDead ) {
			return;
		}
        _isDead = true;
        SR.enabled = false;
        Explosion.SetActive(true);
        var timedDestroy = gameObject.AddComponent<TimedDestroy>();
        timedDestroy.Activate(DESTROY_TIME);
        EventManager.Fire<Event_Enemy_Killed>(new Event_Enemy_Killed());
    }

	public void DelayedDie(float delay) {
		Invoke("Die", delay);
	}
}
