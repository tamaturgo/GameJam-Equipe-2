using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{

    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpForce;
    [SerializeField] private Joystick _joystick;
    [SerializeField] private GameObject shotBullet;
    [SerializeField] private float shotTimerDelay;
    private float shotTimer;
    [SerializeField] private float bulletAjustmentY;
    [SerializeField] private float bulletAjustmentX;
    private float inputHorizontal;
    private float inputVertical;
    public bool isJumping;
    public bool doubleJump;
    public bool isSquating;
    public bool canWalk;
    public bool canJump;

    private Rigidbody2D playerRig;
    private Transform playerSprite;
    private Animator playerAnim;
    private SpriteRenderer gunSprite;
    private SoundController bulletSound;

    private void Start()
    {
        canWalk = true;
        playerRig = GetComponent<Rigidbody2D>();
        playerSprite = GetComponent<Transform>();
        playerAnim = GetComponent<Animator>();
        gunSprite = transform.GetChild(0).GetComponent<SpriteRenderer>();
        //bulletSound = new SoundController();
        //bulletSound.BulletSound();

    }

    private void Update()
    {
        inputHorizontal = _joystick.Horizontal;
        inputVertical = _joystick.Vertical;

        shotTimer += Time.deltaTime;
        // Jump
        Squat();
        if (canWalk)
        {
            Walk();
        }
        else
        {
            playerRig.velocity = new Vector2(playerRig.velocity.x/250, playerRig.velocity.y);
        }
    }

    private void FixedUpdate()
    {
      
    }

    private void Walk()
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
            

        }

        playerRig.AddForce(new Vector2(inputHorizontal * moveSpeed * 5f,0) , ForceMode2D.Force);
        if (playerRig.velocity.x > moveSpeed)
        {
            playerRig.velocity =new Vector2(moveSpeed,playerRig.velocity.y);
        }
        if (playerRig.velocity.x < -moveSpeed)
        {
            playerRig.velocity =new Vector2(-moveSpeed,playerRig.velocity.y);
        }

    }
    public void Jump()
    {

        if (canJump)
        {
            if (!isJumping)
            {
                FindObjectOfType<SoundController>().JumpSound();
                doubleJump = true;
                isJumping = true;
                playerAnim.SetBool("jump", true);
                playerRig.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
            }

            else
            {
                if (doubleJump)
                {
                    FindObjectOfType<SoundController>().JumpSound();
                    doubleJump = false;
                    playerAnim.SetBool("jump", true);
                    playerRig.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
                }
            }
        }
    }

    public void Shot()
    {
        playerRig.velocity = new Vector2(0, playerRig.velocity.y);
        if (shotTimer> shotTimerDelay)
        {
            FindObjectOfType<SoundController>().BulletSound();
            shotTimer = 0;
            gunSprite.enabled = true;
            if (transform.rotation.y == -1)
            {
                Instantiate(shotBullet, new Vector3(transform.position.x - bulletAjustmentX,transform.position.y + bulletAjustmentY, transform.position.z), Quaternion.identity);    
            }
            else
            {
                Instantiate(shotBullet, new Vector3(transform.position.x + bulletAjustmentX,transform.position.y + bulletAjustmentY, transform.position.z), Quaternion.identity);
            }
        
            playerAnim.SetTrigger("shot");
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

    /*private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.layer == 6)
        {
            isJumping = false;
            playerAnim.SetBool("jump", false);
        }
    }
    */

    private void OnTriggerStay2D(Collider2D other)
    {
        canJump = true;
        if (other.gameObject.layer == 6)
        {
            isJumping = false;
            playerAnim.SetBool("jump", false);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (isJumping) canJump = true;
        else canJump = false;
    }

    public void setWalk()
    {
        canWalk = !canWalk;
    }
    
}
