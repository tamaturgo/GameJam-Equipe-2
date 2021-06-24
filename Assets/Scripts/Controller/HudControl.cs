using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HudControl : MonoBehaviour
{
    [SerializeField]
    private Slider playerHP;

    [SerializeField] private GameObject boss;
    [SerializeField] private GameObject HudBoss;
    [SerializeField] private Player _player;
    [SerializeField] private Boss _boss;
    [SerializeField] private Slider _bossSlider;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        playerHP.value = _player.getHp();
        if (boss.activeInHierarchy)
        {
            _bossSlider.value = _boss.BossGetHP();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            boss.SetActive(true);
            HudBoss.SetActive(true);   
        }
    }
}
