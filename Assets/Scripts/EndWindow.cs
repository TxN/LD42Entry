using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public sealed class EndWindow : MonoBehaviour {
    public Button CloseButton = null;

    void Start() {
        CloseButton.onClick.AddListener(OnButtonClick);
    }

    void OnButtonClick() {
		SceneManager.LoadScene("TitleScreen");
	}
}
