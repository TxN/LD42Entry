using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdvertSpawner : MonoSingleton<AdvertSpawner> {

    public List<GameObject> AdvertsPrefabs;

    int CurrentAdvertPrefab = 1;
    public AnimationCurve  AdvertSpawnTimeLowCurve  = new AnimationCurve();
    public AnimationCurve  AdvertSpawnTimeHighCurve  = new AnimationCurve();

    float _nextSpawnTime = 10;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        UpdateTime();
	}

    void UpdateTime() {
        var time = GameState.Instance.GameTime;
        if ( time > _nextSpawnTime ) {
            Spawn(); 
            _nextSpawnTime = Random.Range(AdvertSpawnTimeLowCurve.Evaluate(time), AdvertSpawnTimeHighCurve.Evaluate(time));
        }
    }

    public void Spawn() { 
        int advertToSpawnIndex = 0;

        if ( CurrentAdvertPrefab < AdvertsPrefabs.Count ) {
            advertToSpawnIndex = CurrentAdvertPrefab - 1; 
            CurrentAdvertPrefab++;
        } else {
            advertToSpawnIndex = Random.Range(0, AdvertsPrefabs.Count - 1); 
        }

        GameObject advert = AdvertsPrefabs[advertToSpawnIndex];
        RectTransform canvas = this.gameObject.GetComponent<Canvas>().GetComponent<RectTransform>();
        Transform canvasParent = this.gameObject.GetComponent<Canvas>().GetComponent<Transform>();
        //Vector2 advertPos = new Vector2(
        //    Random.Range(0f, canvas.rect.width - advert.GetComponent<RectTransform>().rect.width),
        //    Random.Range(0f, canvas.rect.height - advert.GetComponent<RectTransform>().rect.height)
        //); 
        Vector2 advertPos = new Vector2(
            Random.Range(0f, canvas.sizeDelta.x - advert.GetComponent<RectTransform>().sizeDelta.x),
            Random.Range(0f, canvas.sizeDelta.y - advert.GetComponent<RectTransform>().sizeDelta.y)
        ); 
        //Instantiate(advert, advertPos, Quaternion.identity, canvasParent); 
        var adv = Instantiate(advert);
        adv.GetComponent<RectTransform>().SetParent(canvasParent);
        adv.GetComponent<RectTransform>().anchoredPosition = advertPos;
        adv.transform.localScale = Vector3.one;
        
    }
}
