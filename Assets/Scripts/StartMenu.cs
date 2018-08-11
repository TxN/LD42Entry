using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour {
	public Button StartButton = null;
	public Toggle SoundToggle = null;

	void Start () {
		SoundToggle.isOn = AudioListener.volume > 0.05f;
		StartButton.onClick.AddListener(LoadLevel);
	}

	void LoadLevel() {
		SceneManager.LoadScene("Level");
	}

	public void OnClickSoundToggle() {
		AudioListener.volume = SoundToggle.isOn ? 1f : 0f;
	}
	
}
