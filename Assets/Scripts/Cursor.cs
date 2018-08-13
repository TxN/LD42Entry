using UnityEngine;

public sealed class Cursor : MonoBehaviour {
	public Camera        Camera     = null;
	public RectTransform MainCanvas = null;

	RectTransform _trans = null;

    void Start() {
        _trans = GetComponent<RectTransform>();
    }

	void Update () {
        var pos = Input.mousePosition;
        Vector2 canvPoint = Vector2.zero;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(MainCanvas, pos, Camera, out canvPoint);
        _trans.anchoredPosition = canvPoint;
	}
}
