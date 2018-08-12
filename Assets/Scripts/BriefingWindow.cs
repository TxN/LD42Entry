using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BriefingWindow : MonoBehaviour {
    public void Hide() {
        var gs = GameState.Instance;
        gs.StartGame();
        Destroy(this.gameObject);
    }
}
