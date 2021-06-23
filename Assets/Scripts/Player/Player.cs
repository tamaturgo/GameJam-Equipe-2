using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]private int life;
    [SerializeField] private int damagePower;
    private Rigidbody2D _rigidbody2D;
    private CapsuleCollider2D caps;
    private EnemyMelle _melleTarget;
    private EnemyRanged _enemyRanged;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Die();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("EnemyMelle"))
        {
            _melleTarget = other.gameObject.GetComponent<EnemyMelle>();
            _melleTarget.EnemyDamage(damagePower);
        }
        if (other.gameObject.CompareTag("EnemyRanged"))
        {
            _enemyRanged = other.gameObject.GetComponent<EnemyRanged>();
            _enemyRanged.EnemyDamage(damagePower);
        }
    }
    
    
    //Damage
    public void TakeDamage(int damage)
    {
        life -= damage;
    }

    private void Die()
    {
        if (life <= 0)
        {
            Destroy(gameObject);
        }
    }
}
