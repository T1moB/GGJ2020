namespace MultiplayerBasicExample
{
    using System.Collections.Generic;
    using InControl;
    using UnityEngine;
    using UnityEngine.UI;

    // This example roughly illustrates the proper way to add multiple players from existing
    // devices. Notice how InputManager.Devices is not used and no index into it is taken.
    // Rather a device references are stored in each player and we use InputManager.OnDeviceDetached
    // to know when one is detached.
    //
    // InputManager.Devices should be considered a pool from which devices may be chosen,
    // not a player list. It could contain non-responsive or unsupported controllers, or there could
    // be more connected controllers than your game supports, so that isn't a good strategy.
    //
    // To detect a joining player, we just check the current active device (which is the last
    // device to provide input) for a relevant button press, check that it isn't already assigned
    // to a player, and then create a new player with it.
    //
    // NOTE: Due to how Unity handles joysticks, disconnecting a single device will currently cause
    // all devices to detach, and the remaining ones to reattach. There is no reliable workaround
    // for this issue. As a result, a disconnecting controller essentially resets this example.
    // In a more real world scenario, we might keep the players around and throw up some UI to let
    // users activate controllers and pick their players again before resuming.
    //
    // This example could easily be extended to use bindings. The process would be very similar,
    // just creating a new instance of your action set subclass per player and assigning the
    // device to its Device property.
    //

    public class PlayerManager : MonoBehaviour
    {
        public GameObject playerPrefab;

        const int maxPlayers = 2;
        private bool player1Ready = false, player2Ready = false;
        [SerializeField] GameObject[] playerUI;
        public Texture readyTexture;
        private bool gameStarted = false;

        public List<Transform> playerPositions = new List<Transform>();

        public List<PlayerMovement> players = new List<PlayerMovement>(maxPlayers);

        void Start()
        {
            InputManager.OnDeviceDetached += OnDeviceDetached;
        }


        void Update()
        {
            var inputDevice = InputManager.ActiveDevice;

            if (JoinButtonWasPressedOnDevice(inputDevice))
            {
                if (ThereIsNoPlayerUsingDevice(inputDevice))
                {
                    CreatePlayer(inputDevice);
                }
            }
            if(players.Count >=2)
            {
                if (players[0].Device.AnyButtonWasPressed && !player1Ready)
                {
                    player1Ready = true;
                    playerUI[0].GetComponentInChildren<RawImage>().texture = readyTexture;
                }
                if (players[1].Device.AnyButtonWasPressed && !player2Ready)
                {
                    player2Ready = true;
                    playerUI[1].GetComponentInChildren<RawImage>().texture = readyTexture;
                }
                new WaitForSeconds(0.5f);
            }
            if(player1Ready && player2Ready && !gameStarted)
            {
                gameStarted = true;
                players[0].canMove = true;
                players[1].canMove = true;
                foreach(ConveyorBelt con in FindObjectsOfType<ConveyorBelt>())
                {
                    con.StartSpawning();
                }
                foreach(GameObject g in playerUI)
                {
                    Destroy(g);
                }
            }
        }


        bool JoinButtonWasPressedOnDevice(InputDevice inputDevice)
        {
            return inputDevice.Action1.WasPressed || inputDevice.Action2.WasPressed || inputDevice.Action3.WasPressed || inputDevice.Action4.WasPressed;
        }


        PlayerMovement FindPlayerUsingDevice(InputDevice inputDevice)
        {
            var playerCount = players.Count;
            for (var i = 0; i < playerCount; i++)
            {
                var player = players[i];
                if (player.Device == inputDevice)
                {
                    return player;
                }
            }

            return null;
        }


        bool ThereIsNoPlayerUsingDevice(InputDevice inputDevice)
        {
            return FindPlayerUsingDevice(inputDevice) == null;
        }


        void OnDeviceDetached(InputDevice inputDevice)
        {
            var player = FindPlayerUsingDevice(inputDevice);
            if (player != null)
            {
                RemovePlayer(player);
            }
        }

        PlayerMovement CreatePlayer(InputDevice inputDevice)
        {
            if (players.Count < maxPlayers)
            {
                if (players.Count < maxPlayers - 1)
                {
                    playerUI[1].SetActive(true);
                }
                // Pop a position off the list. We'll add it back if the player is removed.
                var playerPosition = playerPositions[0];
                playerPositions.RemoveAt(0);

                var gameObject = (GameObject)Instantiate(playerPrefab, playerPosition.position, Quaternion.identity);
                var player = gameObject.GetComponent<PlayerMovement>();
                player.Device = inputDevice;
                players.Add(player);
                return player;
            }

            return null;
        }


        void RemovePlayer(PlayerMovement player)
        {
            playerPositions.Insert(0, player.transform);
            players.Remove(player);
            player.Device = null;
            Destroy(player.gameObject);
        }
    }
}