using System.Collections.Generic;
using UnityEngine;
using EventSys;

public class MonsterSpawner : MonoBehaviour {
    public List<Transform> SpawnPoints = new List<Transform>();
    public List<Enemy>     EnemyFabs   = new List<Enemy>();
    public AnimationCurve  SpawnTimeCurve  = new AnimationCurve();
    public AnimationCurve  SpawnCountCurve = new AnimationCurve();
    public Animator DoorAnimator = null;

    bool  _enabled     = false;
    float _timeToSpawn = 3f;
    int   _spawnCount  = 1;
    bool  _doorOpen    = false;

    void Start() {
        EventManager.Subscribe<Event_Game_Started>(this, OnGameStarted);
        EventManager.Subscribe<Event_Game_Over>(this, OnGameOver);
    }

    void OnDestroy() {
        EventManager.Unsubscribe<Event_Game_Started>(OnGameStarted);
        EventManager.Unsubscribe<Event_Game_Over>(OnGameOver);
    }

    void Update() {
        var gm = GameState.Instance;
        if (!gm.IsPause && _enabled) {
            _timeToSpawn -= Time.deltaTime;
            if (_timeToSpawn < 0.8f && !_doorOpen) {
                DoorOpen();
            }
            if (_timeToSpawn <= 0) {
                Spawn();
            }
        }
    }

    void Spawn() {
        var time = GameState.Instance.GameTime;


        for (int i = 0; i < SpawnPoints.Count; i++) {
            var temp = SpawnPoints[i];
            int randomIndex = Random.Range(i, SpawnPoints.Count);
            SpawnPoints[i] = SpawnPoints[randomIndex];
            SpawnPoints[randomIndex] = temp;
        }

        for (int i = 0; i < _spawnCount; i++) {
            CreateEnemy(SpawnPoints[i].position);
        }

        _timeToSpawn = SpawnTimeCurve.Evaluate(time);
        _spawnCount = Random.Range(1, Mathf.CeilToInt(SpawnCountCurve.Evaluate(time)));
        DoorClose();

    }

    void DoorClose() {
        _doorOpen = false;
        DoorAnimator.SetBool("Open", false);
    }
    void DoorOpen() {
        _doorOpen = true;
        DoorAnimator.SetBool("Open", true);
    }

    void CreateEnemy(Vector3 position) {
        var enemy = Instantiate(EnemyFabs[Random.Range(0, EnemyFabs.Count)], position, Quaternion.identity);

    }

    void OnGameStarted(Event_Game_Started e) {
        _enabled = true;
    }

    void OnGameOver(Event_Game_Over e) {
        _enabled = false;
    }


}
