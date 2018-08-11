using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : MonoSingleton<GameState> {
    public Player         Player = null;
    public MonsterSpawner MonsterSpawner = null;
    


    public bool IsPause = true;

    float _gameTime = 0;
    bool _isStarted = false;

    public float GameTime {
        get {
            return _gameTime;
        }
    }

    void Start() {
        StartGame();
    }

    public void StartGame() {
        if (_isStarted) {
            return;
        }
        _isStarted = true;
        _gameTime = 0f;
    }

    void Update() {
        if (_isStarted && !IsPause) {
            _gameTime += Time.deltaTime;
        }
    }


}
