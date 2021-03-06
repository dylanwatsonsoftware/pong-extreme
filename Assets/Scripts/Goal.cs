using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Goal : NetworkBehaviour
{
    public string player;

    void Start()
    {
        
    }

    void Update()
    {
        
    }


    [ServerCallback]
    void OnTriggerEnter(Collider other) {
        other.GetComponent<Ball>().ReturnToCenter();

        var paddle = GameObject.FindWithTag(player).GetComponent<Paddle>();
        var score = GameObject.Find(player + " Score").GetComponent<Score>();

        Debug.Log("Enter! " + other.name);
        Debug.Log("enemyPaddle! " + paddle.name);
        Debug.Log("enemyPaddle score! " + paddle.score);
        Debug.Log("client: " + !isServer);
        paddle.score++;
        score.SetScore(paddle.score.ToString());
        
        Debug.Log("enemyPaddle score after! " + paddle.score);
    }
}
