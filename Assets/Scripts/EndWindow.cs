using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndWindow : MonoBehaviour {
    public Button CloseButton = null;

    void Start() {
        CloseButton.onClick.AddListener(OnButtonClick);
    }

    void OnButtonClick() {
        Application.Quit();
    }
}
