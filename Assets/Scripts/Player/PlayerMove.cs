using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{

    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpForce;
    [SerializeField] private Joystick _joystick;
    private float inputHorizontal;
    private float inputVertical;
    public bool isJumping;
    public bool doubleJump;
    public bool isSquating;

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
        inputHorizontal = _joystick.Horizontal;
        inputVertical = _joystick.Vertical;
        
        // Jump

        Squat();
    }

    private void FixedUpdate()
    {
        
        // Walk

        if (inputHorizontal > 0.1 && isSquating == false)
        {
            
            playerAnim.SetBool("walk", true);
            playerSprite.eulerAngles =  new Vector3(0, 0, 0);
            gunSprite.enabled = true;
        }
        
        else if (inputHorizontal < -0.1 && isSquating == false)
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
    
    private void Squat(){
        
        if(inputVertical < -0.8 && isJumping == false)
        {
            isSquating = true;
            inputHorizontal = 0;
            gunSprite.enabled = true;
            playerAnim.SetBool("squat", true);
        }
        else
        {
            isSquating = false;
            playerAnim.SetBool("squat", false);
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
