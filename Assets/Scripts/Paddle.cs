using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;

public class Paddle : NetworkBehaviour
{
  public bool top;
  public int score = 0;
  public String left, right;

  float speed = 6;

    private Touch theTouch;
    private Vector2 touchStartPosition, touchEndPosition;
    private string direction;
    private Color darkColor;
    private Color lightColor;

  void Start() {
    darkColor = gameObject.GetComponent<Renderer>().material.color;
    lightColor = new Color(darkColor.r * 1.4f, darkColor.g * 1.4f, darkColor.b * 1.4f);
  }

  void FixedUpdate() {
        if (!isLocalPlayer) return;

            if (Input.GetKey(left)) {
      transform.Translate(new Vector3(-speed * Time.deltaTime, 0, 0));
      Brighten();
    } else if (Input.GetKey(right)) {
      transform.Translate(new Vector3(speed * Time.deltaTime, 0, 0));
       ResetColor();
    }

    if (Input.touchCount > 0) {
      theTouch = Input.GetTouch(0);

      if (theTouch.phase == TouchPhase.Began) {
        touchStartPosition = theTouch.position;
        bool touchedMySide = top ? touchStartPosition.y > Screen.height / 2 : touchStartPosition.y < Screen.height / 2;
        if (touchedMySide){
           Brighten();
        }
      } else if (theTouch.phase == TouchPhase.Moved || theTouch.phase == TouchPhase.Ended) {
          if(theTouch.phase == TouchPhase.Ended) {
              ResetColor();
          }

        bool touchedMySide = top ? touchStartPosition.y > Screen.height / 2 : touchStartPosition.y < Screen.height / 2;

        float x = theTouch.position.x - touchEndPosition.x;
        float y = theTouch.position.y - touchEndPosition.y;

        touchEndPosition = theTouch.position;

        if (touchedMySide) {
            if (Mathf.Abs(x) < 0.01 && Mathf.Abs(y) < 0.01) {
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
        gameObject.GetComponent<Renderer>().material.color = lightColor;
  }

  void ResetColor() {
        gameObject.GetComponent<Renderer>().material.color = darkColor;
  }
}
