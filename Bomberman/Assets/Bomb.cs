using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Unity.Netcode;
using System;

public class Bomb : NetworkBehaviour
{

    CircleCollider2D coll;
    public float timeToActiveCollider = 0.5f;
    public GameObject centerExplosion;
    public GameObject directionalExplosion;
    public float tileDistance = 0.5f;
    public int explosionForce;

    public SpriteRenderer sprite;
    public float timeToExplode = 1f;

    // public Player owner;
    public int owner;

    // Start is called before the first frame update
    void Start()
    {
        coll = GetComponent<CircleCollider2D>();
        sprite = GetComponent<SpriteRenderer>();
        // StartCoroutine(activeCollider());
        StartCoroutine(explodeCoroutine());
    }

    // IEnumerator activeCollider()
    // {
    //     yield return new WaitForSeconds(timeToActiveCollider);
    //     explode();
    //     // coll.isTrigger = false;
    //     yield return null;
    // }

    IEnumerator explodeCoroutine()
    {
        yield return new WaitForSeconds(timeToExplode);
        explode();
        // go.GetComponent<NetworkObject>().Spawn();
        // this.GetComponent<NetworkBehaviour>().
        yield return null;
    }
    void explode()
    {
        var positionInGrid = new Vector3(Mathf.Round(transform.position.x), Mathf.Round(transform.position.y));

        createExplosionsServerRpc(positionInGrid);

        // owner.amountOfAvailableBombs++;
        if (IsServer)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {

            destroyServerRpc();
            return;
        }
    }

    [ServerRpc(RequireOwnership = false)]
    void destroyServerRpc()
    {
        Destroy(this.gameObject);
    }

    [ServerRpc(RequireOwnership = false)]
    void createExplosionsServerRpc(Vector3 positionInGrid)
    {
        GameObject go = Instantiate(centerExplosion, positionInGrid, Quaternion.Euler(new Vector3(0, 0, 0)));
        go.GetComponent<NetworkObject>().Spawn();
        for (int i = 1; i < (explosionForce * 2) + 1; i++)
        {
            go = Instantiate(directionalExplosion, new Vector3(positionInGrid.x + (i * tileDistance), positionInGrid.y), Quaternion.Euler(new Vector3(0, 0, 0)));
            go.GetComponent<NetworkObject>().Spawn();
            go = Instantiate(directionalExplosion, new Vector3(positionInGrid.x - (i * tileDistance), positionInGrid.y), Quaternion.Euler(new Vector3(0, 0, 0)));
            go.GetComponent<NetworkObject>().Spawn();
            go = Instantiate(directionalExplosion, new Vector3(positionInGrid.x, positionInGrid.y + (i * tileDistance)), Quaternion.Euler(new Vector3(0, 0, 270)));
            go.GetComponent<NetworkObject>().Spawn();
            go = Instantiate(directionalExplosion, new Vector3(positionInGrid.x, positionInGrid.y - (i * tileDistance)), Quaternion.Euler(new Vector3(0, 0, 270)));
            go.GetComponent<NetworkObject>().Spawn();
        }
    }


    private void createDirectionalExplosions(Vector3 positionInGrid)
    {
        //right
        for (int i = 1; i < (explosionForce * 2) + 1; i++)
        {
            Instantiate(directionalExplosion, new Vector3(positionInGrid.x + (i * tileDistance), positionInGrid.y), Quaternion.Euler(new Vector3(0, 0, 0)));
            Instantiate(directionalExplosion, new Vector3(positionInGrid.x - (i * tileDistance), positionInGrid.y), Quaternion.Euler(new Vector3(0, 0, 0)));
            Instantiate(directionalExplosion, new Vector3(positionInGrid.x, positionInGrid.y + (i * tileDistance)), Quaternion.Euler(new Vector3(0, 0, 270)));
            Instantiate(directionalExplosion, new Vector3(positionInGrid.x, positionInGrid.y - (i * tileDistance)), Quaternion.Euler(new Vector3(0, 0, 270)));
        }

        //left
        // for (int i = 1; i < (bombForce * 2) + 1; i++)
        // {
        // }

        // //up
        // for (int i = 1; i < (bombForce * 2) + 1; i++)
        // {
        // }

        // //down
        // for (int i = 1; i < (bombForce * 2) + 1; i++)
        // {
        // }
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag.Equals("explosion"))
        {
            explode();
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag.Equals("Player"))
        {
            coll.isTrigger = false;
            sprite.color = Color.white;
        }
    }
}
