using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
public class PowerUp : NetworkBehaviour
{
    public bool isBeingDestroyed = false;
    public PowersEnum powerUpEnum;
    // Start is called before the first frame update

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.tag.Equals("Player"))
        {
            return;
        }
        if (IsServer)
        {
            Destroy(this.gameObject);
        }
        else
        {

            destroyServerRpc();
        }
    }

    [ServerRpc(RequireOwnership = false)]
    public void destroyServerRpc()
    {
        Destroy(this.gameObject);
        this.gameObject.SetActive(false);
    }
}
