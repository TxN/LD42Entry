using UnityEngine;
using EventSys;

public class Player : MonoBehaviour {
    public int        Health               = 100;
    public GameObject ProjectilePrefab     = null;
    public Transform  ProjectileSpawnPoint = null;
    public Transform  HandImg              = null;
    public AudioSource ShootSound          = null;

    int  _maxHealth = 0;
    bool _isDead    = false;

    public float MinYAngle = -10;
    public float MaxYAngle = 10;
	public float MinXAngle = -20;
	public float MaxXAngle = 20;

    public float MinXHandOffset = -100;
    public float MaxXHandOffset = 100;
    public float MinYHandOffset = -30;
    public float MaxYHandOffset = 30;
    
    Projectile _currentProjectile = null;
    Vector3    _handImgInitPos    = Vector3.zero;

	const int ADVERT_CLOSE_HP_BOOST = 5;

    void Start() {
        _maxHealth = Health;
        _handImgInitPos = HandImg.position;
		EventManager.Subscribe<Event_UI_Window_Closed>(this, OnWindowClosed);
    }

    void Update() {
        var gs = GameState.Instance;
        if (gs.IsStarted && !_isDead) {
            float xHandOffset = Input.mousePosition.x;
            float yHandOffset = Input.mousePosition.y;
            xHandOffset = xHandOffset.Map(0, Screen.width, MinXHandOffset, MaxXHandOffset);
            yHandOffset = yHandOffset.Map(0, Screen.height, MinYHandOffset, MaxYHandOffset);

            HandImg.position = _handImgInitPos + new Vector3(xHandOffset,yHandOffset,0);

            if (_currentProjectile == null) {
                _currentProjectile = Instantiate(ProjectilePrefab, ProjectileSpawnPoint.position, Quaternion.identity, ProjectileSpawnPoint).GetComponent<Projectile>();
            } else {
                if (Input.GetMouseButtonDown(0) && _currentProjectile.CanShoot) {

                    float yAngle = Input.mousePosition.x;
					float xAngle = Input.mousePosition.y;

                    yAngle = yAngle.Map(0, Screen.width, MinYAngle, MaxYAngle);
					xAngle = xAngle.Map(0, Screen.height, MinXAngle, MaxXAngle);
                    _currentProjectile.transform.Rotate(-xAngle, yAngle, 0);
                    _currentProjectile.transform.parent = null;
                    _currentProjectile.Fire();
                    ShootSound.Play();
                    _currentProjectile = null;
                }
            }
        }
    }

	private void OnDestroy() {
		EventManager.Unsubscribe<Event_UI_Window_Closed>(OnWindowClosed);
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

    public float HealthPercent {
        get {
            return (float)Health / _maxHealth;
        }
    }

	void OnWindowClosed(Event_UI_Window_Closed e) {
		if (e.IsAdvert && !_isDead) {
			Health = Mathf.Clamp(Health + ADVERT_CLOSE_HP_BOOST, 0, _maxHealth);
		}
	}
}
