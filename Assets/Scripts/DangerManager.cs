using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DangerManager : MonoBehaviour
{
    private Vector3 startingPos = new Vector3(0, 0);
    
    private int timeAfterDeath = -1;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Danger"))
        {
            transform.position = startingPos;
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);

            timeAfterDeath = 0;
            GameObject.FindGameObjectWithTag("Black").GetComponent<SpriteRenderer>().enabled = true;
            GetComponent<PlayerMovement>().enabled = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("Checkpoint"))
        {
            startingPos = collision.gameObject.transform.position;
        }
    }

    private void Update()
    {
        if (timeAfterDeath != -1)
        {
            timeAfterDeath++;
        }
        if (timeAfterDeath > (int)(1f / Time.unscaledDeltaTime) * 4)
        {
            timeAfterDeath = -1;

            GameObject.FindGameObjectWithTag("Black").GetComponent<SpriteRenderer>().enabled = false;
            GetComponent<PlayerMovement>().enabled = true;
        }
    }
}
