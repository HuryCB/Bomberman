using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Unity.Netcode;

public class DestructibleWall : NetworkBehaviour
{
    public List<GameObject> loot;
    public int powerUpToInstantiate;
    public bool isBeingDestroyed = false;
    // Start is called before the first frame update
    void Start()
    {
        powerUpToInstantiate = Random.Range(0, loot.Count * 2);
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag.Equals("explosion"))
        {
            if (isBeingDestroyed)
            {
                return;
            }
            isBeingDestroyed = true;
            Debug.Log("parede colidiu com explosao");
            if (IsServer)
            {
                Debug.Log("destruindo objeto");


                if (powerUpToInstantiate < loot.Count)
                {
                    GameObject go = Instantiate(loot[powerUpToInstantiate], this.transform.position, Quaternion.Euler(new Vector3(0, 0, 0)));
                    go.GetComponent<NetworkObject>().Spawn();
                }
                Destroy(this.gameObject);
                return;
            }
            // else
            // {
            //     Debug.Log("server destroy");
            //     DestroyServerRpc();
            // }
        }
    }

    // [ServerRpc(RequireOwnership = false)]
    // private void DestroyServerRpc()
    // {
    //     foreach (var item in loot)
    //     {
    //         GameObject go = Instantiate(item, this.transform.position, Quaternion.Euler(new Vector3(0, 0, 0)));
    //         go.GetComponent<NetworkObject>().Spawn();
    //     }
    //     Destroy(this.gameObject);
    // }
}
