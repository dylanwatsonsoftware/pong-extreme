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


  void FixedUpdate() {
    if (!isLocalPlayer) return;

    if (Input.GetKey(left)) {
      transform.Translate(new Vector3(-speed * Time.deltaTime, 0, 0));
    } else if (Input.GetKey(right)) {
      transform.Translate(new Vector3(speed * Time.deltaTime, 0, 0));
    }
  }
}
