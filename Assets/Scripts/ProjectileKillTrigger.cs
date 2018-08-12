using UnityEngine;

public class ProjectileKillTrigger : MonoBehaviour {
    void OnTriggerEnter(Collider other) {
        var projectile = other.GetComponent<Projectile>();
        if (projectile) {
            projectile.KillProjectile();
        }
    }
}
