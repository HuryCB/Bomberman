using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Unity.Netcode;
using System;
using UnityEngine.UI;

public class GameManager : NetworkBehaviour
{
    public static GameManager instance;
    //public static MatchManager matchInstance;
    public List<Player> players;
    public int playersAlive;

    //public Button buttonStartGame;
 
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
        DontDestroyOnLoad(this.gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log("GameManager start");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Backspace))
        {
            if (!IsServer)
            {
                return;
            }
            //Debug.Log("Começando jogo");
            this.startGame();
        }
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("OnSceneLoaded: " + scene.name);
        Debug.Log(mode);
    }

    public void startGame()
    {
        foreach (Player player in players)
        {
           
            ClientRpcParams clientRpcParams = new ClientRpcParams
            {
                Send = new ClientRpcSendParams
                {
                    TargetClientIds = new ulong[] { player.OwnerClientId }
                }
            };
            enablePlayerClientRpc(player.OwnerClientId, clientRpcParams);
        }
    }

    public void restartGame()
    {
        SpanwerManager.instance.resetPositions();
        //teste
        SpanwerManager.instance.choosePlayerspos();

        foreach (Player player in players)
        {
            player.gameObject.SetActive(true);
            ClientRpcParams clientRpcParams = new ClientRpcParams
            {
                Send = new ClientRpcSendParams
                {
                    TargetClientIds = new ulong[] { player.OwnerClientId }
                }
            };
            restartPlayerClientRpc(player.OwnerClientId, clientRpcParams);
        }
    }

    //public void startGameAndDisableButton()
    //{
    //    this.startGame();
    //    //ButtonManager.instance.buttonIniciarPartida.gameObject.SetActive(false);
    //}


    [ClientRpc]
    private void restartPlayerClientRpc(ulong id, ClientRpcParams clientRpcParams)
    {
       
        foreach (var player in GameManager.instance.players)
        {
            if (player.OwnerClientId == id)
            {
                //Debug.Log("habilitando" + id);
                //if (SceneManager.GetActiveScene().name.Equals("WaitingScene"))
                //{
                    //Debug.Log("ativando");
                    player.gameObject.SetActive(true);
                player.walkSpeed = 2.0f;
                player.amountOfAvailableBombs = 1;
                player.explosionForce = 1;
                //}
                //else
                //{

                player.enabled = false;
                //}
                return;
            }
        }
        if (IsServer)
        {
            SpanwerManager.instance.choosePlayerspos();
        }
    }

    [ClientRpc]
    private void enablePlayerClientRpc(ulong id, ClientRpcParams clientRpcParams)
    {
        foreach (var player in GameManager.instance.players)
        {
            //if (player.OwnerClientId == id)
            //{
                //Debug.Log("habilitando" + id);
                //if (SceneManager.GetActiveScene().name.Equals("WaitingScene"))
                //{
                //    Debug.Log("ativando");
                //    player.gameObject.SetActive(true);
                //}
                //else
                //{

                player.enabled = true;
                //}
            //    return;
            //}
        }
    }


    internal void onPlayerDeath()
    {
        if (IsServer)
        {
            //Debug.Log("Player Death");
            this.playersAlive--;
            if(playersAlive <= 1)
            {
                //Debug.Log("Deveria mudar a cena");
                NetworkManager.SceneManager.LoadScene("WaitingScene", LoadSceneMode.Single);
            }
        }
        }
}
