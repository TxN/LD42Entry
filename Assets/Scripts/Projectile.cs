using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {
    public int     Damage        = 10;
    public float   Speed         = 10f;
    public Vector3 MoveDirection = new Vector3(0, 0, 1);

    void Update() {
        transform.Translate(MoveDirection * Speed * Time.deltaTime);
    }

    void OnTriggerEnter(Collider other) {
        var enemy = other.GetComponent<Enemy>();
        if (enemy) {
            enemy.DoDamage(Damage);
        }
    }
}
