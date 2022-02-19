using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trampoline : MonoBehaviour
{
    [SerializeField] public Animator anim;
    private int framesAfterBouncing = -1;


    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.CompareTag("Player"))
        {
            anim.SetBool("bounce", true);
            framesAfterBouncing = 0;
        }
    }

    private void Update()
    {
        if (anim.GetBool("bounce") && framesAfterBouncing == 0)
        {
            framesAfterBouncing = 1;
        }
        if (framesAfterBouncing > 1)
        {
            framesAfterBouncing = -1;
            anim.SetBool("bounce", false);
        }
        if (framesAfterBouncing > -1)
        {
            framesAfterBouncing++;
        }

        
    }
}
