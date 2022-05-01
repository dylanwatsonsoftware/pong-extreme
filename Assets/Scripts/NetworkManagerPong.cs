using UnityEngine;
using UnityEngine.UI;
using Mirror;

/*
	Documentation: https://mirror-networking.gitbook.io/docs/components/network-manager
	API Reference: https://mirror-networking.com/docs/api/Mirror.NetworkManager.html
*/

namespace Mirror.Examples.Pong
{
    // Custom NetworkManager that simply assigns the correct racket positions when
    // spawning players. The built in RoundRobin spawn method wouldn't work after
    // someone reconnects (both players would be on the same side).
    [AddComponentMenu("")]
    public class NetworkManagerPong : NetworkManager
    {

        public Transform leftRacketSpawn;
        public Transform rightRacketSpawn;

        public Text leftScore;
        public Text rightScore;

        public Goal leftGoal;
        public Goal rightGoal;

        GameObject ball;

        public override void OnServerAddPlayer(NetworkConnectionToClient conn)
        {
            // add player at correct spawn position
            Transform start = numPlayers == 0 ? leftRacketSpawn : rightRacketSpawn;
            Text score = numPlayers == 0 ? leftScore : rightScore;
            Goal goal = numPlayers == 0 ? rightGoal : leftGoal;

            GameObject player = Instantiate(playerPrefab, start.position, start.rotation);
            Paddle paddle = player.GetComponent<Paddle>();
            paddle.SetColourByNumber(numPlayers);
            paddle.SetScoreText(score);
            goal.enemyPaddle = paddle;
            player.name = "Paddle " + numPlayers;
            NetworkServer.AddPlayerForConnection(conn, player);

            Debug.Log("Player added");


            // spawn ball if two players
            if (numPlayers == 2)
            {
                Debug.Log("2 players! Creating a ball");

                var ballPrefab = spawnPrefabs.Find(prefab => prefab.name == "Ball3D");
                ball = Instantiate(ballPrefab);
                NetworkServer.Spawn(ball);
            }
        }

        public override void OnServerDisconnect(NetworkConnectionToClient conn)
        {
            // destroy ball
            if (ball != null)
                NetworkServer.Destroy(ball);

            // call base functionality (actually destroys the player)
            base.OnServerDisconnect(conn);
        }
    }
}
