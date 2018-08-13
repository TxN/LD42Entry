using UnityEngine;

public sealed class EscapeButtonHandler : MonoBehaviour {
	void Start () {
		DontDestroyOnLoad(gameObject);
	}

	void Update () {
		if ( Input.GetKeyDown(KeyCode.Escape)) {
			Application.Quit();
		}
	}
}
