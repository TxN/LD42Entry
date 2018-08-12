using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlideshowAnim : MonoBehaviour {
    public List<Sprite> Sprites = new List<Sprite>();
    public List<float>  Delays = new List<float>();

    float _time = 0f;
    float _nextFrameTime = 0f;
    int _frameIndex = 0;
    SpriteRenderer _renderer = null;

    void Start() {
        _renderer = GetComponent<SpriteRenderer>();
        _nextFrameTime =Time.time + Delays[_frameIndex];
        _renderer.sprite = Sprites[0];
        _frameIndex++;
    }

    void Update() {
        _time += Time.deltaTime;
        if (Time.time > _nextFrameTime) {
            _renderer.sprite = Sprites[_frameIndex];
            _nextFrameTime = Time.time + Delays[_frameIndex];
            _frameIndex++;
            if (_frameIndex >= Sprites.Count) {
                _frameIndex = 0;
            }
        }
    }
}
