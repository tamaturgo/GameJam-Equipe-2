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
    private GameController gc;
    
    // Start is called before the first frame update
    void Start()
    {
        gc = FindObjectOfType<GameController>();
    }

    // Update is called once per frame
    void Update()
    {
        Die();
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
            gc.StarNewGame("GameOver");
        }
    }

    public int getHp()
    {
        return life;
    }
}
