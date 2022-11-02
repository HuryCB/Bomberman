using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
public abstract class PowerUp : NetworkBehaviour
{
    public PowersEnum powerUpEnum;
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
        if (other.tag.Equals("Player"))
        {
            Debug.Log("colidiu com player");
        }
    }

    [ServerRpc(RequireOwnership = false)]
    public void destroyServerRpc()
    {
        Destroy(this.gameObject);
        this.gameObject.SetActive(false);

        // IncreaseOwnerAmountOfAvailableBombsClientRpc(owner, clientRpcParams);
    }
}
