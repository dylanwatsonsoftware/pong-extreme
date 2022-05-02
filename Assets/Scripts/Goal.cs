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


    void OnTriggerEnter(Collider other) {
        if(!isServer) {
            Debug.Log("Enter! " + other.name);
            Debug.Log("enemyPaddle! " + enemyPaddle.name);
            Debug.Log("enemyPaddle score! " + enemyPaddle.score);
            Debug.Log("client: " + !isServer);
            enemyPaddle.score++;
            other.GetComponent<Ball>().ReturnToCenter();
            Debug.Log("enemyPaddle score after! " + enemyPaddle.score);
        }
    }
}
