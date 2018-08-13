using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using EventSys;

public sealed class Advert : MonoBehaviour { 

    public Button CloseButton;

    public float NewWindowProbability = 0.5f;

	Sequence _seq = null;

	void Start () {
        CloseButton.onClick.AddListener(Close);
		_seq = TweenHelper.ReplaceSequence(_seq);
		var curScale = transform.localScale;
		transform.localScale = Vector3.zero;
		_seq.Append(transform.DOScale(curScale, 0.2f));
		_seq.Append(transform.DOPunchScale(new Vector3(0.05f, 0.05f, 0), 0.25f));
	}

	public void DelayedClose(float time) {
		Invoke("Close", time);
	}
	
    void Close() {
		EventManager.Fire<Event_UI_Window_Closed>(new Event_UI_Window_Closed() { IsAdvert = true });
		_seq = TweenHelper.ReplaceSequence(_seq);
		_seq.Append(transform.DOScale(0, 0.15f));
		_seq.AppendInterval(0.1f);
		_seq.AppendCallback(() => { Destroy(this.gameObject); });	
    }

    void Misclick() {
        //SOUND STUFF WILL BE HERE 

        if ( Random.Range(0, 1) > NewWindowProbability ) {
            AdvertSpawner.Instance.Spawn(); 
        } 
    }

    public void ClickButton() {
        AdvertSpawner.Instance.Spawn();
    }

	private void OnDestroy() {
		_seq = TweenHelper.ResetSequence(_seq);
	}
}
