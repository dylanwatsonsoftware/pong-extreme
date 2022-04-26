using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Paddle: MonoBehaviour {
  public int score = 0;
  public String left, right;

  float speed = 5;

    private Touch theTouch;
    private Vector2 touchStartPosition, touchEndPosition;
    private string direction; 

  void Start() {

  }

  void Update() {
    if (Input.GetKey(left)) {
      transform.Translate(new Vector3(-speed * Time.deltaTime, 0, 0));
    } else if (Input.GetKey(right)) {
      transform.Translate(new Vector3(speed * Time.deltaTime, 0, 0));
    }

    if (Input.touchCount > 0) {
      theTouch = Input.GetTouch(0);

      if (theTouch.phase == TouchPhase.Began) {
        touchStartPosition = theTouch.position;
      } else if (theTouch.phase == TouchPhase.Moved || theTouch.phase == TouchPhase.Ended) {

        float x = theTouch.position.x - touchEndPosition.x;
        float y = theTouch.position.y - touchEndPosition.y;

        touchEndPosition = theTouch.position;

        if (Mathf.Abs(x) == 0 && Mathf.Abs(y) == 0) {
          direction = "Tapped";
        } else if (Mathf.Abs(x) > Mathf.Abs(y)) {
            direction = x > 0 ? "Right" : "Left";

            if(x > 0) {
                transform.Translate(new Vector3(speed * Time.deltaTime, 0, 0));
            } else {
                transform.Translate(new Vector3(-speed * Time.deltaTime, 0, 0));
            }
            
        } else {
          direction = y > 0 ? "Up" : "Down";
        }
      }
    }
  }
}
