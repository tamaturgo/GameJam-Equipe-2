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
        Vector3 normalizedDistance = (_player.transform.position - transform.position).normalized;
        _rigidbody2D.velocity = normalizedDistance * shotSpeed;
        if (_rigidbody2D.velocity.x < 0)
        {
            _spriteRenderer.flipX = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _player.TakeDamage(damage);
        }
    }
}
