using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Advert : MonoBehaviour { 

    public Button CloseButton;

    public float NewWindowProbability = 0.5f;

	// Use this for initialization
	void Start () {
        CloseButton.onClick.AddListener(Close);
		
	}
	
	// Update is called once per frame
	void Update () { 
	}

    void Close() {
        Destroy(this.gameObject);
    }

    void Misclick() {
        //SOUND STUFF WILL BE HERE 

        if ( Random.Range(0, 1) > NewWindowProbability ) {
            AdvertSpawner.Instance.Spawn();
            
        } 
    }
}
