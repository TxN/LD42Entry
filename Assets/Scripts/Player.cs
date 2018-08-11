using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EventSys;

public class Player : MonoBehaviour {
    public int        Health               = 100;
    public GameObject ProjectilePrefab     = null;
    public Transform  ProjectileSpawnPoint = null;
    int _maxHealth = 0;
    bool _isDead = false;

    public float MinYAngle = -10;
    public float MaxYAngle = 10;
	public float MinXAngle = -20;
	public float MaxXAngle = 20;
    
    Projectile _currentProjectile = null;

    void Start() {
        _maxHealth = Health;
    }

    void Update() {
        var gs = GameState.Instance;
        if (gs.IsStarted && !_isDead) {
            if (_currentProjectile == null) {
                _currentProjectile = Instantiate(ProjectilePrefab, ProjectileSpawnPoint.position, Quaternion.identity).GetComponent<Projectile>();
            } else {
                if (Input.GetMouseButtonDown(0) && _currentProjectile.CanShoot) {

                    float yAngle = Input.mousePosition.x;
					float xAngle = Input.mousePosition.y;
                    yAngle = yAngle.Map(0, Screen.width, MinYAngle, MaxYAngle);
					xAngle = xAngle.Map(0, Screen.height, MinXAngle, MaxXAngle);
                    _currentProjectile.transform.Rotate(-xAngle, yAngle, 0);
                    _currentProjectile.Fire();
                    _currentProjectile = null;
                }
            }
        }
    }

    void Die() {
		_isDead = true;
        EventManager.Fire<Event_Game_Over>(new Event_Game_Over());
    }

    void OnTriggerEnter(Collider other) {
        var enemy = other.GetComponent<Enemy>();
        if ( enemy ) {
            Health = Mathf.Clamp(Health - enemy.AttackDamage, 0, _maxHealth);
            enemy.StopMoving();
            if (Health == 0 && !_isDead ) {
                Die();
            }
        }
    }
}
