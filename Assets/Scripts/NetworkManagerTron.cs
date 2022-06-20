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
    public class NetworkManagerTron : NetworkManager
    {

        public Transform leftRacketSpawn;
        public Transform rightRacketSpawn;

        public Text leftScore;
        public Text rightScore;

        public override void OnServerAddPlayer(NetworkConnectionToClient conn)
        {
            Debug.Log("Connected");
            // add player at correct spawn position
            Transform start = numPlayers == 0 ? leftRacketSpawn : rightRacketSpawn;
            Text score = numPlayers == 0 ? leftScore : rightScore;

            GameObject player = Instantiate(playerPrefab, start.position, start.rotation);
            int playerNum = (numPlayers + 1);
            player.name = "Paddle " + playerNum;
            player.tag = "P" + playerNum;
            Debug.Log("Connected " + ((numPlayers == 0) ? "Player 1" : "Player 2"));
            NetworkServer.AddPlayerForConnection(conn, player);

            if (numPlayers == 1)
            {
                GameObject.Find("P1 Score").GetComponent<Score>().SetScore("Connected!");
            }
            if (numPlayers == 2)
            {
                GameObject.Find("P2 Score").GetComponent<Score>().SetScore("Connected!");
            }
        }
    }
}
