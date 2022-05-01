using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;

[RequireComponent(typeof(Text))]
public class Score : NetworkBehaviour {
    
    [SyncVar]
    string score;

    public Text text;

    void Start()
    {
        text = GetComponent<Text>();
    }

    void Update() {
        text.text = score;
    }

    public void SetScore(string score) {
        this.score = score;
    }
}
