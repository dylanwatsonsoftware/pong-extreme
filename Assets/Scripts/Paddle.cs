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
                GoLeft();
            } else {
                GoRight();
            }
        } else {
            direction = y > 0 ? "Up" : "Down";
        }
      }
    }
  }

  void GoLeft() {
    AddForce(new Vector3(-speed * Time.deltaTime, 0, 0));
  }

  void GoRight() {
    AddForce(new Vector3(speed * Time.deltaTime, 0, 0));
  }

  void AddForce(Vector3 forceDir) {
    if (isServer)
    {
        Debug.Log("is server");
        RpcAddClientForce(ForceMode.Impulse, forceDir);
    }
    else
    {
        Debug.Log("is client");
        CmdAddForce(ForceMode.Impulse, forceDir);
    }
  }

    [Command]
    public void CmdAddForce(ForceMode forcemode, Vector3 forceVector)
    {
        Debug.Log("command add force");
        GetComponent<Rigidbody>().MovePosition(transform.position + forceVector);
    }
 
    [ClientRpc]
    public void RpcAddClientForce(ForceMode forcemode, Vector3 forceVector)
    {
        Debug.Log("clientrpc add force");
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
}
