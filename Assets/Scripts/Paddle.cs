using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    public int score = 0;
    public String left, right;

    float speed = 3;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(left)) {
            transform.Translate(new Vector3(-speed * Time.deltaTime, 0, 0));
        } else if(Input.GetKey(right)) {
            transform.Translate(new Vector3(speed * Time.deltaTime, 0, 0));
        }
    }
}
