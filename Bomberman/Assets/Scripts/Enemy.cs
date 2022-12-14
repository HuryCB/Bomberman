using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    AudioSource basicSound;
    public GameObject blood;
    private GameObject Player;

    public GameObject player;

    public float speed = 1.5f;
    // Start is called before the first frame update
    void Start()
    {
        basicSound = GetComponent<AudioSource>();
        Player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {

            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "activateBox")
        {
            if (!basicSound.isPlaying)
            {
                basicSound.Play();
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "bullet")
        {

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "bullet")
        {

        }
    }

    public void damage()
    {
        // Instantiate(blood, new Vector3(bulletPosition.position.x,bulletPosition.position.y,bulletPosition.position.z),Quaternion.identity);
        GameObject bloodCopy = Instantiate(blood);
        bloodCopy.transform.position = this.transform.position;

    }
}
