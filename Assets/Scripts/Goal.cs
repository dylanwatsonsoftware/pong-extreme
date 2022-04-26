using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{

    public Paddle enemyPaddle;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other) {
        enemyPaddle.score++;
        GameObject.Find("Ball").GetComponent<Ball>().ReturnToCenter();
    }
}
