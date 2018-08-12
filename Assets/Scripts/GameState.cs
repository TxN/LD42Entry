using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EventSys;

public class GameState : MonoSingleton<GameState> {
    public Player         Player         = null;
    public MonsterSpawner MonsterSpawner = null;
    public BriefingWindow BriefingWindow = null;


    public bool IsPause = true;

    float _gameTime = 0;
    bool _isStarted = false;
    
    public float GameTime {
        get {
            return _gameTime;
        }
    }

    public bool IsStarted {
        get {
            return _isStarted;
        }
    }

    void Start() {
        BriefingWindow.gameObject.SetActive(true);
    }

    public void StartGame() {
        if (_isStarted) {
            return;
        }
        _isStarted = true;
        IsPause = false;
        _gameTime = 0f;
        EventManager.Fire<Event_Game_Started>(new Event_Game_Started());
    }

    void Update() {
        if (_isStarted && !IsPause) {
            _gameTime += Time.deltaTime;
        }
    }


}
