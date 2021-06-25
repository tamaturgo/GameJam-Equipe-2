using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : MonoBehaviour
{
    private Player player;


    private void Start() {
        player = FindObjectOfType<Player>();
    }

    void OnTriggerEnter2D(Collider2D col) {

       if(col.CompareTag("Player"))
       {
           player.TakeDamage(player.getHp());
       }
    }
}
