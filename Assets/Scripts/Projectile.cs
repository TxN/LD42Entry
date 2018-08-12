using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class Projectile : MonoBehaviour {
    public int        Damage        = 10;
    public float      Speed         = 10f;
    public Vector3    MoveDirection = new Vector3(0, 0, 1);
    public GameObject Explosion     = null;

    Sequence _seq      = null;
    bool     _isReady  = false;
    bool     _isMoving = false;
    Vector3  _initScale = Vector3.zero;

    void Start() {
        _initScale = transform.localScale;

        _seq = TweenHelper.ReplaceSequence(_seq);
        transform.localScale = Vector3.zero;
        _seq.Append(transform.DOScale(_initScale,0.3f));
        _seq.Append(transform.DOPunchScale(new Vector3(0.1f,0.1f,0.1f),0.1f));
        _seq.AppendCallback(() => {_isReady = true;});
    }

    void Update() {
        if (_isMoving) {
            transform.Translate(MoveDirection * Speed * Time.deltaTime);
        }   
    }

    void OnTriggerEnter(Collider other) {
        var enemy = other.GetComponent<Enemy>();
        if (enemy) {
            enemy.DoDamage(Damage);
            KillProjectile();
        }
    }

    void OnDestroy() {
        _seq = TweenHelper.ResetSequence(_seq);

    }

    public bool CanShoot {
        get {
            return _isReady && !_isMoving;
        }
    }

    public void KillProjectile() {
        _isMoving = false;
        Explosion.transform.parent = null;
        var destr = Explosion.AddComponent<TimedDestroy>();
        destr.Activate(1.5f);
        Explosion.gameObject.SetActive(true);
        Destroy(this.gameObject);
    }

    public bool Fire() {
        if ( !_isMoving && _isReady ) {
            _isMoving = true;
            return true;
        } else {
            return false;
        }
    }
}
