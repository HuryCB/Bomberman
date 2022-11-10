using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Unity.Netcode;

public class ButtonManager : NetworkBehaviour
{
    public Button buttonIniciarPartida;

    private void Start()
    {
        if (!IsServer)
        {
            buttonIniciarPartida.gameObject.SetActive(false);
        }
    }
}
