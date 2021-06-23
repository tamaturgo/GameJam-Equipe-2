using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    private Vector2 velocity;
    [SerializeField] private float shotSpeed;
    [SerializeField] private int damage;
    private Player _player;
    private Vector2 distance;
    private SpriteRenderer _spriteRenderer;
    private EnemyMelle _melleTarget;
    private EnemyRanged _enemyRanged;
    private Boss _boss;
    

    private Rigidbody2D _rigidbody2D;
    // Start is called before the first frame update
    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _player = FindObjectOfType<Player>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        setVelocity();
        Destroy(gameObject, 5f);
    }

    private void setVelocity()
    {
        if (gameObject.CompareTag("Player_bullet"))
        {
            
            if (_player.transform.rotation.y == 1 || _player.transform.rotation.y == -1)
            {
                _rigidbody2D.AddForce(new Vector2(-8,0),ForceMode2D.Impulse);
            }
            else
            {
                _rigidbody2D.AddForce(new Vector2(8,0),ForceMode2D.Impulse);
            }
                
            if (_rigidbody2D.velocity.x < 0)
            {
                _spriteRenderer.flipX = true;
            }  
        }
        else
        {
            Vector3 normalizedDistance = (_player.transform.position - transform.position).normalized;
            _rigidbody2D.velocity = normalizedDistance * shotSpeed;
            if (_rigidbody2D.velocity.x < 0)
            {
                _spriteRenderer.flipX = true;
            }    
        }
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _player.TakeDamage(damage);
            
        }
        else
        {
            if (other.CompareTag("EnemyMelle"))
            {
                _melleTarget = other.gameObject.GetComponent<EnemyMelle>();
                _melleTarget.EnemyDamage(damage);
            }
            if (other.CompareTag("EnemyRanged"))
            {
                _enemyRanged = other.gameObject.GetComponent<EnemyRanged>();
                _enemyRanged.EnemyDamage(damage);
            }
            if (other.gameObject.CompareTag("Shield"))
            {
                
            }
            if (other.gameObject.CompareTag("Boss"))
            {
                _boss = other.gameObject.GetComponent<Boss>();
                _boss.BossDamage(damage);
            }
            Destroy(gameObject);
        }
        
    }
}
