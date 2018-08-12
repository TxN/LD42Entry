using UnityEngine;

public class Cursor : MonoBehaviour {
    RectTransform _trans = null;
    public Camera Camera = null;
    public RectTransform MainCanvas = null;
    void Start() {
        _trans = GetComponent<RectTransform>();
    }

	void Update () {
        var pos = Input.mousePosition;
        Vector2 canvPoint = Vector2.zero;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(MainCanvas, pos, Camera, out canvPoint);
        var width = Screen.width / 2;
        var height = Screen.height / 2;
        _trans.anchoredPosition = canvPoint;
	}
}
