using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWalkingHitBox : MonoBehaviour
{
    // Start is called before the first frame update

    public bool isColliding = false;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    // private void OnTriggerStay(Collider other)
    // {
    //     if (other.tag.Equals("wall"))
    //     {
    //         isColliding = true;
    //     }
    // }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag.Equals("wall"))
        {
            isColliding = true;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag.Equals("wall"))
        {
            isColliding = false;
        }
    }

    private void OnTriggerStay2D(Collider2D other) {
         if (other.tag.Equals("wall"))
        {
            isColliding = true;
            // Destroy(other.gameObject);
            // other.gameObject.
        }
    }
}
