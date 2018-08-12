using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdvertSpawner : MonoSingleton<AdvertSpawner> {

    public List<GameObject> AdvertsPrefabs;

    public List<AudioClip> WindowSounds = new List<AudioClip>();

    int CurrentAdvertPrefab = 1;
    public AnimationCurve  AdvertSpawnTimeLowCurve  = new AnimationCurve();
    public AnimationCurve  AdvertSpawnTimeHighCurve  = new AnimationCurve();

    float _nextSpawnTime = 20;

    AudioSource _audioSource = null;

    void Start() {
        _audioSource = GetComponent<AudioSource>();
    }

	void Update () {
        var gs = GameState.Instance;
        if (gs.IsStarted && !gs.IsPause) {
            UpdateTime();
        }
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

        if ( CurrentAdvertPrefab <= AdvertsPrefabs.Count ) {
            advertToSpawnIndex = CurrentAdvertPrefab - 1; 
            CurrentAdvertPrefab++;
        } else {
            advertToSpawnIndex = Random.Range(0, AdvertsPrefabs.Count - 1); 
        }

        GameObject advert = AdvertsPrefabs[advertToSpawnIndex];
        RectTransform canvas = this.gameObject.GetComponent<Canvas>().GetComponent<RectTransform>();
        Transform canvasParent = this.gameObject.GetComponent<Canvas>().GetComponent<Transform>();
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
}
