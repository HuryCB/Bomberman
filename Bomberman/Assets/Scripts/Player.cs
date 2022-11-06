using System.Collections;
using UnityEngine;
using Unity.Netcode;

public class Player : NetworkBehaviour
{
    Rigidbody2D rb;

    AudioSource stepSound;
    float horizontal;
    float vertical;

    public float walkSpeed = 2.0f;
    public float runSpeed = 3.0f;
    public float speed = 0;
    bool idle = true;

    // bool canPlantBomb = true;
    public float plantingTime = 1f;

    public GameObject bomb;

    public int amountOfAvailableBombs = 1;

    public int explosionForce = 1;


    public bool canPlantBomb()
    {
        return amountOfAvailableBombs > 0;
    }
    public override void OnNetworkSpawn()
    {
        GameManager.instance.players.Add(this);
        // if(!IsOwner){
        //     Destroy(this);
        // }
    }

    // Start is called before the first frame update
    void Start()
    {
        if (!IsOwner)
        {
            return;
        }
        this.gameObject.SetActive(false);
        rb = GetComponent<Rigidbody2D>();
        stepSound = GetComponent<AudioSource>();
        Debug.Log(OwnerClientId);
    }

    // Update is called once per frame
    void Update()
    {
        if (!IsOwner)
        {
            return;
        }
        move();

        checkRun();

        checkPlantBomb();
    }

    private void checkPlantBomb()
    {
        if (!canPlantBomb())
        {
            return;
        }
        if (Input.GetKey(KeyCode.Space))
        {
            StartCoroutine(plantBomb());
        }
    }

    IEnumerator plantBomb()
    {
        amountOfAvailableBombs--;
        // canPlantBomb();

        plantBombServerRpc(OwnerClientId, explosionForce);
        yield return new WaitForSeconds(plantingTime);
        // amountOfAvailableBombs++;
        // canPlantBomb = true;
        yield return null;
    }
    [ServerRpc(RequireOwnership = false)]
    private void plantBombServerRpc(ulong id, int explosiongForce)
    {
        var positionInGrid = new Vector3(Mathf.Round(transform.position.x), Mathf.Round(transform.position.y));
        GameObject go = Instantiate(bomb, positionInGrid, Quaternion.identity);
        // plantingTime = go.GetComponent<Bomb>().timeToExplode;

        go.GetComponent<NetworkObject>().Spawn();
        go.GetComponent<Bomb>().setOwnerServerRpc(id);
        go.GetComponent<Bomb>().setExplosionForceServerRpc(explosiongForce);
    }

    private void move()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");
        if (rb.velocity.x != 0 || rb.velocity.y != 0)
        {
            idle = false;
        }
        else
        {
            idle = true;
        }

        if (idle)
        {
            stepSound.Stop();
        }
        else
        {
            if (!stepSound.isPlaying)
            {
                stepSound.Play();
            }
        }
    }

    private void checkRun()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            speed = runSpeed;
            stepSound.pitch = runSpeed / walkSpeed;
        }
        else
        {
            speed = walkSpeed;
            stepSound.pitch = walkSpeed / walkSpeed;
        }
    }
    private void FixedUpdate()
    {
        if (!IsOwner)
        {
            return;
        }
        rb.velocity = new Vector2(horizontal * speed, vertical * speed);
    }

    void OnTriggerStay2D(Collider2D col)
    {
        if (!IsOwner)
        {
            return;
        }
        if (col.tag == "door")
        {
            if (Input.GetKey(KeyCode.E))
            {
                // col.transform.parent.gameObject.GetComponent<NetworkObject>().Despawn();
                // Destroy(col.transform.parent.gameObject);
                GameObject door = col.transform.gameObject;
                door.GetComponent<Door>().OpenDoor();

            }
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        switch (other.tag)
        {
            case "PowerUp":

                PowerUp powerup = other.gameObject.GetComponent<PowerUp>();
                powerup.destroyServerRpc();
                this.explosionForce++;
                Debug.Log("relou no power up: " + powerup);
                break;
        }

    }

    // [ClientRpc]
    // private void OpenDoorClientRpc(){

    // }



}
