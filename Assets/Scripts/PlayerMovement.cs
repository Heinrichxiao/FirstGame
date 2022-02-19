using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer sprite;
    private float dirX = 0f;
    public int falling;
    public bool isTouchingGround;

    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Terrain" || collision.gameObject.tag == "MovingPlatform")
        {
            isTouchingGround = true;
        }
        if (collision.gameObject.tag == "Spike")
        {
            Debug.Log("Game Over!");
        }
        if (collision.gameObject.CompareTag("Trampoline"))
        {
            rb.velocity = new Vector2(rb.velocity.x, 20f);
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Terrain" || collision.gameObject.tag == "MovingPlatform")
        {
            isTouchingGround = false;
        }
    }
    private void UpdateAnimationState()
    {
        decimal dirY = Decimal.Round(new Decimal(rb.velocity.y), 2);
        if (dirY > 0)
        {
            anim.SetBool("isJumping", true);
            anim.SetBool("isRunning", false);
            anim.SetBool("isIdle", false);
            anim.SetBool("isFalling", false);
        }
        else if (dirY < 0)
        {

            anim.SetBool("isJumping", false);
            anim.SetBool("isRunning", false);
            anim.SetBool("isIdle", false);
            anim.SetBool("isFalling", true);
        }
        else if (dirX > 0f)
        {
            anim.SetBool("isJumping", false);
            anim.SetBool("isRunning", true);
            anim.SetBool("isIdle", false);
            anim.SetBool("isFalling", false);
            sprite.flipX = false;
        }
        else if (dirX < 0f)
        {
            anim.SetBool("isJumping", false);
            anim.SetBool("isRunning", true);
            anim.SetBool("isIdle", false);
            anim.SetBool("isFalling", false);
            sprite.flipX = true;
        }
        else
        {
            anim.SetBool("isJumping", false);
            anim.SetBool("isRunning", false);
            anim.SetBool("isIdle", true);
            anim.SetBool("isFalling", false);
        }
    }

    // Update is called once per frame
    private void Update()
    {
        dirX = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(dirX * 7f, rb.velocity.y);

        if (Input.GetButtonDown("Jump") && falling < 3)
        {
            rb.velocity = new Vector2(rb.velocity.x, 11f);
        }
        if (isTouchingGround)
        {
            falling = 0;
        }
        else
        {
            falling++;
        }
        transform.rotation = Quaternion.Euler(0, 0, 0);
        UpdateAnimationState();
    }
}