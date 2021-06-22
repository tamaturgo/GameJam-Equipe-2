using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{

    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpForce;
    
    private float inputHorizontal;
    public bool isJumping;
    public bool doubleJump;

    private Rigidbody2D playerRig;
    private Transform playerSprite;
    private Animator playerAnim;
    private SpriteRenderer gunSprite;

    private void Start()
    {
        
        playerRig = GetComponent<Rigidbody2D>();
        playerSprite = GetComponent<Transform>();
        playerAnim = GetComponent<Animator>();
        gunSprite = transform.GetChild(0).GetComponent<SpriteRenderer>();

    }

    private void Update()
    {
        inputHorizontal = Input.GetAxisRaw("Horizontal");
        
        // Jump

        Jump();
    }

    private void FixedUpdate()
    {
        
        // Walk

        if (inputHorizontal > 0.1)
        {
            
            playerAnim.SetBool("walk", true);
            playerSprite.eulerAngles =  new Vector3(0, 0, 0);
            gunSprite.enabled = true;
        }
        
        else if (inputHorizontal < -0.1)
        {
            
            playerAnim.SetBool("walk", true);
            playerSprite.eulerAngles =  new Vector3(0, 180, 0);
            gunSprite.enabled = true;
        }

        else if (inputHorizontal == 0 && isJumping == false)
        {
            
            playerAnim.SetBool("walk", false);
            gunSprite.enabled = false;

        }

        playerRig.velocity = new Vector2(inputHorizontal * moveSpeed, playerRig.velocity.y);

    }

    private void Jump()
    {
        if (Input.GetButtonDown("Jump"))
        {
            gunSprite.enabled = true;
            if (!isJumping)
            {
                doubleJump = true;
                isJumping = true;
                playerAnim.SetBool("jump", true);
                playerRig.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
            }

            else
            {
                if (doubleJump)
                {
                    doubleJump = false;
                    playerAnim.SetBool("jump", true);
                    playerRig.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
                }
            }
        }
        
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.layer == 6)
        {
            isJumping = false;
            playerAnim.SetBool("jump", false);
        }
    }
    
}
