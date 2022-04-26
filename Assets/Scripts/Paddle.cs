using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Paddle: MonoBehaviour {
  public bool top;
  public int score = 0;
  public String left, right;

  float speed = 6;

    private Touch theTouch;
    private Vector2 touchStartPosition, touchEndPosition;
    private string direction;
    private Color dark;
    private Color light;

  void Start() {
    dark = gameObject.GetComponent<Renderer>().material.color;
    light = new Color(dark.r * 1.4f, dark.g * 1.4f, dark.b * 1.4f);
  }

  void FixedUpdate() {
    if (Input.GetKey(left)) {
      transform.Translate(new Vector3(-speed * Time.deltaTime, 0, 0));
      Brighten();
    } else if (Input.GetKey(right)) {
      transform.Translate(new Vector3(speed * Time.deltaTime, 0, 0));
      Brighten();
    } else {
      ResetColor();
    }

    if (Input.touchCount > 0) {
      theTouch = Input.GetTouch(0);

      if (theTouch.phase == TouchPhase.Began) {
        touchStartPosition = theTouch.position;
        Brighten();
      } else if (theTouch.phase == TouchPhase.Moved || theTouch.phase == TouchPhase.Ended) {
          if(theTouch.phase == TouchPhase.Ended) {
              ResetColor();
          }

        bool touchedMySide = top ? touchStartPosition.y > Screen.height / 2 : touchStartPosition.y < Screen.height / 2;

        float x = theTouch.position.x - touchEndPosition.x;
        float y = theTouch.position.y - touchEndPosition.y;

        touchEndPosition = theTouch.position;

        if (touchedMySide) {
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

  void Brighten() {
        gameObject.GetComponent<Renderer>().material.color = light;
  }

  void ResetColor() {
        gameObject.GetComponent<Renderer>().material.color = dark;
  }
}
