using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Unity.Netcode;
using System;

public class DestructibleWall : NetworkBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag.Equals("explosion"))
        {
            Debug.Log("parede colidiu com explosap");
            if (IsServer)
            {
                Debug.Log("destruindo objeto");
                Destroy(this.gameObject);
                return;
            }
            else
            {
                Debug.Log("server destroy");
                DestroyServerRpc();
            }
        }
    }

    [ServerRpc(RequireOwnership = false)]
    private void DestroyServerRpc()
    {
        Destroy(this.gameObject);
    }
}
