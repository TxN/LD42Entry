﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EventSys;

public class GameState : MonoSingleton<GameState> {
    public Player         Player         = null;
    public MonsterSpawner MonsterSpawner = null;
    public BriefingWindow BriefingWindow = null;
    public EndWindow      EndWindow      = null;
    public FadeScreen     Fader          = null;


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
        UnityEngine.Cursor.visible = false;
        BriefingWindow.gameObject.SetActive(true);
        EventManager.Subscribe<Event_Game_Over>(this, OnGameOver);
    }

    void OnDestroy() {
        EventManager.Unsubscribe<Event_Game_Over>(OnGameOver);
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

    void OnGameOver(Event_Game_Over e) {
        Fader.FadeBlack(1);
        Invoke("ShowEndWindow", 1.2f);
    }

    void ShowEndWindow() {
        EndWindow.transform.SetAsLastSibling();
        EndWindow.gameObject.SetActive(true);
    }


}
