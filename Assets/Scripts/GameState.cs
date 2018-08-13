using UnityEngine;
using EventSys;

public sealed class GameState : MonoSingleton<GameState> {
    public Player         Player         = null;
    public MonsterSpawner MonsterSpawner = null;
    public BriefingWindow BriefingWindow = null;
    public EndWindow      EndWindow      = null;
	public WinWindow      WinWindow      = null;
    public FadeScreen     Fader          = null;

    public bool IsPause = true;

    float _gameTime  = 0;
    bool  _isStarted = false;
	bool  _isWin     = false;
	bool  _isFail    = false;

	const float WIN_TIME = 200;
    
    public float GameTime {
        get {
            return _gameTime;
        }
    }

    public bool IsStarted {
        get {
            return _isStarted;
        }
    }

    void Start() {
        UnityEngine.Cursor.visible = false;
        BriefingWindow.gameObject.SetActive(true);
        EventManager.Subscribe<Event_Game_Over>(this, OnGameOver);
    }

    void OnDestroy() {
        EventManager.Unsubscribe<Event_Game_Over>(OnGameOver);
    }

    public void StartGame() {
        if (_isStarted) {
            return;
        }
        _isStarted = true;
        IsPause = false;
        _gameTime = 0f;
        EventManager.Fire<Event_Game_Started>(new Event_Game_Started());
    }

    void Update() {
        if (_isStarted && !IsPause) {
            _gameTime += Time.deltaTime;
			if (!_isFail && (_gameTime > WIN_TIME) && !_isWin) {
				WinGame();
			}
        }
    }

    void OnGameOver(Event_Game_Over e) {
        Fader.FadeBlack(1);
        Invoke("ShowEndWindow", 1.2f);
		_isFail = true;
    }

    void ShowEndWindow() {
        EndWindow.transform.SetAsLastSibling();
        EndWindow.gameObject.SetActive(true);
    }

	void WinGame() {
		_isWin = true;
		EventManager.Fire<Event_Game_Win>(new Event_Game_Win());
		var ads = FindObjectsOfType<Advert>();
		for (int i = 0; i < ads.Length; i++) {
			ads[i].DelayedClose( (i+1) * 0.25f);
		}
		var enemies = FindObjectsOfType<Enemy>();
		for (int i = 0; i < enemies.Length; i++) {
			enemies[i].DelayedDie((i + 2) * 0.1f);
		}
		Fader.FadeBlack(1.5f);
		Invoke("ShowWinWindow", 1f);


	}

	void ShowWinWindow() {
		WinWindow.gameObject.SetActive(true);
	}


}
