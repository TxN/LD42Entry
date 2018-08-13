using System.Collections.Generic;
using UnityEngine;
using EventSys;

public sealed class AdvertSpawner : MonoSingleton<AdvertSpawner> {
	public List<GameObject> AdvertsPrefabs            = new List<GameObject>();
    public List<AudioClip>  WindowSounds              = new List<AudioClip>(); 
    public AnimationCurve   AdvertSpawnTimeLowCurve   = new AnimationCurve();
    public AnimationCurve   AdvertSpawnTimeHighCurve  = new AnimationCurve();

    AudioSource _audioSource         = null;
	Canvas      _thisCanvas          = null;
	float       _nextSpawnTime       = 20;
	int         _currentAdvertPrefab = 1;
	bool        _enabled             = true;

	void Start() {
        _audioSource = GetComponent<AudioSource>();
		_thisCanvas = gameObject.GetComponent<Canvas>();
		EventManager.Subscribe<Event_Game_Win>(this, OnGameWin);
	}

	void Update () {
        var gs = GameState.Instance;
        if (gs.IsStarted && !gs.IsPause && _enabled) {
            UpdateTime();
        }
	}

	void OnDestroy() {
		EventManager.Unsubscribe<Event_Game_Win>(OnGameWin);
	}

	void UpdateTime() {
        var time = GameState.Instance.GameTime;
        if ( time > _nextSpawnTime ) {
            Spawn(); 
            _nextSpawnTime = time + Random.Range(AdvertSpawnTimeLowCurve.Evaluate(time), AdvertSpawnTimeHighCurve.Evaluate(time));
        }
    }

    public void Spawn() { 
        int advertToSpawnIndex = 0;

        if ( _currentAdvertPrefab <= AdvertsPrefabs.Count ) {
            advertToSpawnIndex = _currentAdvertPrefab - 1; 
            _currentAdvertPrefab++;
        } else {
            advertToSpawnIndex = Random.Range(0, AdvertsPrefabs.Count - 1); 
        }

        GameObject advert = AdvertsPrefabs[advertToSpawnIndex];
	
		RectTransform canvas       = _thisCanvas.GetComponent<RectTransform>();
        Transform     canvasParent = _thisCanvas.transform;
        Vector2 advertPos = new Vector2(
            Random.Range(0f, canvas.sizeDelta.x - advert.GetComponent<RectTransform>().sizeDelta.x),
            Random.Range(0f, canvas.sizeDelta.y - advert.GetComponent<RectTransform>().sizeDelta.y)
        ); 
        var adv = Instantiate(advert);
        adv.GetComponent<RectTransform>().SetParent(canvasParent);
        adv.transform.localPosition = Vector3.zero;
        adv.GetComponent<RectTransform>().anchoredPosition = advertPos;
        adv.transform.localScale = Vector3.one;

        _audioSource.clip = WindowSounds[Random.Range(0, WindowSounds.Count)];
        _audioSource.Play();
    }

	void OnGameWin(Event_Game_Win e) {
		_enabled = false;
	}
}
