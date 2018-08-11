using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    public int Health = 100;

    int _maxHealth = 0;
    bool _isDead = false;

    void Start() {
        _maxHealth = Health;
    }

    void Die() {
    
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
