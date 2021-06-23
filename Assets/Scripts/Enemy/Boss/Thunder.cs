using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thunder : MonoBehaviour
{
    [SerializeField] private int _thunderDamage;
    private Player _player;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 1.6f);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _player = other.gameObject.GetComponent<Player>();
            _player.TakeDamage(_thunderDamage );
        }
    }
}
