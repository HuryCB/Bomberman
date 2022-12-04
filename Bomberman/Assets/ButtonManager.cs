using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Unity.Netcode;
using UnityEngine.SceneManagement;

public class ButtonManager : NetworkBehaviour
{
    public static ButtonManager instance;
    public Button buttonIniciarPartida;
    public GameObject labelWaitingHost;

    private void Awake()
    {
        //if (instance != null && instance != this)
        //{
        //    Destroy(this);
        //}
        //else
        //{
        //    instance = this;
        //}
        //DontDestroyOnLoad(this.gameObject);
    }

    private void Start()
    {
        if (!IsHost)
        {
            Debug.Log("n é server");
            buttonIniciarPartida.gameObject.SetActive(false);
        }
        else
        {
            buttonIniciarPartida.gameObject.SetActive(true);
            labelWaitingHost.SetActive(false);
            //Debug.Log("é server");
        }
    }

    public void buttonIniciarPartidaAction()
     {
        buttonIniciarPartida.gameObject.SetActive(false);
        if (SceneManager.GetActiveScene().name.Equals("WaitingScene"))
        {
            SpanwerManager.instance.resetPositions();
            GameManager.instance.restartGame();
            NetworkManager.SceneManager.LoadScene("SampleScene", LoadSceneMode.Single);
            return;
        }
        if (IsServer)
        {
            GameManager.instance.playersAlive = GameManager.instance.players.Count;
        }
        foreach (var player in GameManager.instance.players)
        {
            ClientRpcParams clientRpcParams = new ClientRpcParams
            {
                Send = new ClientRpcSendParams
                {
                    TargetClientIds = new ulong[] { player.OwnerClientId }
                }
            };
            deactiveClientRpc(clientRpcParams);
        }

        GameManager.instance.startGame();
    }


    [ClientRpc]
    public void deactiveClientRpc(ClientRpcParams clientRpcParams = default)
    {
        labelWaitingHost.SetActive(false);
    }
}
