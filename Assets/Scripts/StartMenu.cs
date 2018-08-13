using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public sealed class StartMenu : MonoBehaviour {
	public Button StartButton    = null;
    public Button SoundButton    = null;
	public Button ExitButton     = null;
    public Sprite SoundOnSprite  = null;
    public Sprite SoundOffSprite = null;

    bool _soundOn = false;

	void Start () {
		UnityEngine.Cursor.visible = true;

        _soundOn = AudioListener.volume > 0.05f;
        SoundButton.image.sprite = _soundOn ? SoundOnSprite : SoundOffSprite;
		StartButton.onClick.AddListener(LoadLevel);
        SoundButton.onClick.AddListener(OnClickSoundToggle);
		ExitButton.onClick.AddListener(OnClickExit);
		if ( Application.platform == RuntimePlatform.WebGLPlayer ) {
			ExitButton.gameObject.SetActive(false);
		}
	}

	void LoadLevel() {
		SceneManager.LoadScene("Level");
	}

	public void OnClickSoundToggle() {
        _soundOn = !_soundOn;
        SoundButton.image.sprite = _soundOn ? SoundOnSprite : SoundOffSprite;
		AudioListener.volume = _soundOn ? 1f : 0f;
	}

	void OnClickExit() {
		Application.Quit();
	}
}
