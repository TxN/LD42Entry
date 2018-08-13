using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;
public sealed class WinWindow : MonoBehaviour {
	Sequence _seq = null;
	
	void Start() {
		var initScale = transform.localScale;
		transform.localScale = Vector3.zero;
		_seq = TweenHelper.ReplaceSequence(_seq);
		_seq.Append(transform.DOScale(initScale, 0.5f));
	}

	private void OnDestroy() {
		_seq = TweenHelper.ResetSequence(_seq);
	}

	public void OnButtonClose() {
		SceneManager.LoadScene("TitleScreen");
	}
}
