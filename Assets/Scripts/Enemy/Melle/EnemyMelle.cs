using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMelle : MonoBehaviour
{
    private Vector2 _distance;
    private Player _playerScript;
    private Rigidbody2D _rigidbody2D;
    [SerializeField] private int visionCamp;
    [SerializeField] private int enemySpeed;
    [SerializeField] private int enemyHp;
    [SerializeField] private int damagePower;
    [SerializeField] private float _damageDelay;
    private Vector2 initalPos;
    private float _damageDelayTimer;
    private float nextVelocity;
    private Animator _animator;
    private SpriteRenderer _renderer;
    
    // Start is called before the first frame update
    void Start()
    {
        _renderer = GetComponent<SpriteRenderer>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _playerScript = FindObjectOfType<Player>();
        _animator = GetComponent<Animator>();
     
    }

    // Update is called once per frame
    void Update()
    {
        
        _distance = _playerScript.transform.position - transform.position;
        _damageDelayTimer += Time.deltaTime;
        Die();
        if (_playerScript.transform.position.x - transform.position.x > 0)
        {
            _renderer.flipX = true;
        }
        else
        {
            _renderer.flipX = false;
        }
    }

    private void FixedUpdate()
    {
        FollowPlayer(_distance, visionCamp);
    }

    // Atack
    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (_damageDelayTimer > _damageDelay)
            {
                _damageDelayTimer = 0;
                AtackPlayer(damagePower);   
            }

        }
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == 6)
        {
            _rigidbody2D.AddForce(new Vector2(0, 10), ForceMode2D.Impulse);
        }


    }

    private void AtackPlayer(int damage)
    {
        _playerScript.TakeDamage(damage);
    }
    
    
    // Damage
    public void EnemyDamage(int damage)
    {
        enemyHp -= damage;
    }

    public void Die()
    {
        if (enemyHp <= 0)
        {
            _rigidbody2D.bodyType = RigidbodyType2D.Static;
            _animator.SetTrigger("hited");
            Destroy(gameObject, 0.44f);
        }
    }
    // Movement 
    private void FollowPlayer(Vector2 distance, int vision)
    {
        
        if (distance[0] > 0)
        {
            if (distance[0] < vision)
            {
                nextVelocity = enemySpeed;
            }
            else
            {
                nextVelocity = 0;
            }
        }
        else
        {
            if (distance[0] > -vision)
            {
                nextVelocity = -enemySpeed;    
            }
            else
            {
                nextVelocity = 0;
            }
        }
        _rigidbody2D.velocity = new Vector2(nextVelocity, _rigidbody2D.velocity.y);
    }
    
}
