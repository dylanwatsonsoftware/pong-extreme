using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

[RequireComponent(typeof(Rigidbody))]
public class Ball : NetworkBehaviour
{
    float speed = 4;
    public Rigidbody rigidBody;

    public override void OnStartServer()
    {
        base.OnStartServer();

        // only simulate ball physics on server
        //rb.simulated = true;

        // Serve the ball from left player
        //rigidbody2d.velocity = Vector2.right * speed;
        rigidBody.AddForce(new Vector3(speed, 0, speed), ForceMode.Impulse);
    }

    public void ReturnToCenter() {
        int velZ = Random.Range(0, 4) > 2 ? Random.Range(-4, -7) : Random.Range(4, 7);

        rigidBody.velocity = new Vector3(4, 0, velZ);
        transform.position = new Vector3(0.1f, 0.2f, 0.2f);
    }
}
