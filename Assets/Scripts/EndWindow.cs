using UnityEngine;
using UnityEngine.UI;

public sealed class EndWindow : MonoBehaviour {
    public Button CloseButton = null;

    void Start() {
        CloseButton.onClick.AddListener(OnButtonClick);
    }

    void OnButtonClick() {
        Application.Quit();
    }
}
