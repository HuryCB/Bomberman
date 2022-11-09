using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Unity.Netcode;
using System;

public class GameManager : NetworkBehaviour
{
    public static GameManager instance;
    public List<Player> players;
 
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
            Debug.Log("Começando jogo");
            this.starGame();
        }
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("OnSceneLoaded: " + scene.name);
        Debug.Log(mode);
    }

    private void starGame()
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

    [ClientRpc]
    private void enablePlayerClientRpc(ulong id, ClientRpcParams clientRpcParams)
    {
        foreach (var player in GameManager.instance.players)
        {
            if (player.OwnerClientId == id)
            {
                Debug.Log("habilitando" + id);
                player.enabled = true;
                return;
            }
        }
    }
}
