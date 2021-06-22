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
    private float _damageDelayTimer;
    private float nextVelocity;
    
    // Start is called before the first frame update
    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _playerScript = FindObjectOfType<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        _distance = _playerScript.transform.position - transform.position;
        _damageDelayTimer += Time.deltaTime;
        Die();
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
            Destroy(gameObject);
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
