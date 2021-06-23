using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthController : MonoBehaviour
{
   public float health = 2f;
   //permitir a variação entre o mínimo valor e 14
   public float Health{
       get {
           return health;
       }

       set {
           health = Mathf.Clamp(value, 0, healthMax);
       }
   }
   public float healthMax = 14f;

    public Image healthBar;

    private void Update() {

        if(Input.GetKey(KeyCode.UpArrow))
        {
            Health += (1f) * Time.deltaTime;
        }

        if(Input.GetKey(KeyCode.DownArrow))
        {
            Health -= (1f) * Time.deltaTime;
        }

        UpdateHealthBar();
    }

    private void UpdateHealthBar()
    {
        healthBar.fillAmount = Health/healthMax;
    }
}
