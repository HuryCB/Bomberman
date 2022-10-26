using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class Explosion : NetworkBehaviour
{
    public float timeToVanish = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(vanish());
        return;
    }

    IEnumerator vanish()
    {
        yield return new WaitForSeconds(timeToVanish);
        // Debug.Log("should have vanished");
        if (IsServer)
        {
            Destroy(this.gameObject);
            yield return null;
        }
        else
        {

            destroyServerRpc();
            yield return null;
        }
    }

    [ServerRpc(RequireOwnership = false)]
    void destroyServerRpc()
    {
        Destroy(this.gameObject);
    }
    // Update is called once per frame
    void Update()
    {

    }
}
