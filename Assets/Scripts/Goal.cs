using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Goal : NetworkBehaviour
{
    [SyncVar]
    public Paddle enemyPaddle;

    void Start()
    {
        
    }

    void Update()
    {
        
    }


    [ServerCallback]
    void OnTriggerEnter(Collider other) {
        if(isServer) {
            enemyPaddle.score++;
            GameObject.Find("Ball3D").GetComponent<Ball>().ReturnToCenter();
        }
    }
}
