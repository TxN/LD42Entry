using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour {

    Image _img = null;

    void Start() {
        _img = GetComponent<Image>();
    }

	void Update () {
        var gs = GameState.Instance;
        _img.fillAmount = gs.Player.HealthPercent;
	}
}
