using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;

public class Paddle : NetworkBehaviour
{
    [SyncVar]
    public Color color;

  [SyncVar]
  public int score = 0;

  public Text scoreText;

  public String left, right;

  float speed = 6;

    private Touch theTouch;
    private Vector2 touchStartPosition, touchEndPosition;
    private string direction;
    private Color darkColor;
    private Color lightColor;

  void Start() {
    darkColor = GetComponent<Renderer>().material.color;
    lightColor = new Color(darkColor.r * 1.4f, darkColor.g * 1.4f, darkColor.b * 1.4f);
    GetComponent<Renderer>().material.color = color;
    GetComponent<Rigidbody>().isKinematic = !isServer;
  }

  void Awake() {
    GetComponent<Renderer>().material.color = color;
  }

    public override void OnStartLocalPlayer()
    {
        GetComponent<Renderer>().material.color = color;
    }

  void FixedUpdate() {
    if (!isLocalPlayer) return;

    if (Input.GetKey(left)) {
      GoLeft();
      Brighten();
    } else if (Input.GetKey(right)) {
      GoRight();
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

        float x = theTouch.position.x - touchEndPosition.x;
        float y = theTouch.position.y - touchEndPosition.y;

        touchEndPosition = theTouch.position;

        if (Mathf.Abs(x) < 0.01 && Mathf.Abs(y) < 0.01) {
            direction = "Tapped";
        } else if (Mathf.Abs(x) > Mathf.Abs(y)) {
            direction = x > 0 ? "Right" : "Left";

            if(x > 0) {
                GoRight();
            } else {
                GoLeft();
            }
        } else {
            direction = y > 0 ? "Up" : "Down";
        }
      }
    }
  }

  void GoLeft() {
    Move(new Vector3(-speed * Time.deltaTime, 0, 0));
  }

  void GoRight() {
    Move(new Vector3(speed * Time.deltaTime, 0, 0));
  }

  void Move(Vector3 forceDir) {
    if (isServer)
    {
        RpcMove(forceDir);
    }
    else
    {
        CmdMove(forceDir);
    }
  }

    [Command]
    public void CmdMove(Vector3 forceVector)
    {
        GetComponent<Rigidbody>().MovePosition(transform.position + forceVector);
    }
 
    [ClientRpc]
    public void RpcMove(Vector3 forceVector)
    {
        GetComponent<Rigidbody>().MovePosition(transform.position + forceVector);
    }


  public void SetColourByNumber(int numPlayers) {
    if(numPlayers == 1) {
      color = Color.blue;
    } else {
      color = Color.red;
    }
    GetComponent<Renderer>().material.color = color;
  }

  void Brighten() {
        GetComponent<Renderer>().material.color = lightColor;
  }

  void ResetColor() {
        GetComponent<Renderer>().material.color = darkColor;
  }

  public void SetScoreText(Text text) {
    this.scoreText = text;
  }
}
