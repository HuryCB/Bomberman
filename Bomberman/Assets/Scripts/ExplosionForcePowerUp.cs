using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class ExplosionForcePowerUp : PowerUp
{
    public bool isBeingDestroyed = false;
    public new PowersEnum powerUpEnum = PowersEnum.ExplosionForce;
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
        // if (isBeingDestroyed)
        // {
        //     return;
        // }
        // isBeingDestroyed = true;
        // this.gameObject.SetActive(false);
        // Destroy(this.gameObject);
    }

    [ServerRpc(RequireOwnership = false)]
    void destroyServerRpc()
    {
        this.gameObject.SetActive(false);
        Destroy(this.gameObject);
    }
}
