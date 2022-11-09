using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using UnityEngine.SceneManagement;

public class ConnectionManager : MonoBehaviour
{
    // Start is called before the first frame update
    public TMP_InputField ipText;
    public TMP_InputField portText;

    NetworkManager m_NetworkManager;
    public UnityTransport m_Transport;

    string m_PortString = "7777";
    string m_ConnectAddress = "127.0.0.1";
    //string ipAuxiliar = "127.0.0.1";

    private void Awake()
    {
        m_NetworkManager = GetComponent<NetworkManager>();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartClient()
    {
        getConnectionData();
        this.m_NetworkManager.StartClient();
    }

    public void StartHost()
    {
        getConnectionData();
        this.m_NetworkManager.StartHost();
        m_NetworkManager.SceneManager.LoadScene("SampleScene", LoadSceneMode.Single);
    }

    public void getConnectionData()
    {
        //m_Transport = (UnityTransport)this.m_NetworkManager.NetworkConfig.NetworkTransport;
        //m_ConnectAddress = this.ipText.text.ToString();
        m_PortString = this.portText.text.ToString();

        //Debug.Log(m_Transport.ConnectionData.Address);
        m_ConnectAddress = (string)this.ipText.text.ToString().Trim();
        
        //Debug.Log(ipAuxiliar);
        //Debug.Log(string.Equals(m_Transport.ConnectionData.Address, ipAuxiliar));
        m_Transport.ConnectionData.ServerListenAddress = "0.0.0.0";
        if (ushort.TryParse(m_PortString, out ushort port))
        {
            m_Transport.SetConnectionData(m_ConnectAddress, port);
        }
        else
        {
            m_Transport.SetConnectionData(m_ConnectAddress, 7777);
        }

      
        //NetworkSceneManager
        //this.m_ConnectAddress = this.ipText.text;
        //this.m_PortString = this.portText.text;
        //Debug.Log(this.m_ConnectAddress);
        //Debug.Log(this.m_PortString);


        //if (ushort.TryParse(m_PortString, out ushort port))
        //{
        //    Debug.Log(port);
        //    m_Transport.SetConnectionData(m_ConnectAddress, port);
        //}
        //else
        //{
        //    m_Transport.SetConnectionData(m_ConnectAddress, 7777);
        //}
    }
}
