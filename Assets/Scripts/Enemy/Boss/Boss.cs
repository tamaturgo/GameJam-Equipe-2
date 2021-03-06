using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = System.Random;

public class Boss : MonoBehaviour
{
    private float jointForceTimer;
    [SerializeField] private float jointForceTimerDelay;
    [SerializeField] private float jointForce;
    [SerializeField] private float shieldTimerDelay;
     private float shieldTimer;
    private Player _player;
    private Animator _animator;
    [SerializeField] private GameObject thunder; 
    [SerializeField] private int bossHP;
    private Rigidbody2D _rigidbody2D;
    private Random randomPos;
    private int posx;
    private SpriteRenderer _renderer;
    
    
    // Live System 
    
    
    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
        _renderer = GetComponent<SpriteRenderer>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        randomPos = new System.Random();
        _player = FindObjectOfType<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        //Idle Movement
        jointForceTimer += Time.deltaTime;
        shieldTimer += Time.deltaTime;
        if (jointForceTimer > jointForceTimerDelay)
        {
            _rigidbody2D.AddForce(new Vector2(1.5f, jointForce), ForceMode2D.Impulse);
            jointForceTimer = 0;
            SpawnThuder();
        }

        if (shieldTimer > shieldTimerDelay)
        {
            
            ShieldActive();
            shieldTimer = 0;
        }

        FlipSprite();


    }

    private void ShieldActive()
    {
        transform.GetChild(0).gameObject.SetActive(true);
        StartCoroutine(DisableShield(6f));
    }

    private void SpawnThuder()
    {
        posx = randomPos.Next((int) _player.transform.position.x - 2, (int) _player.transform.position.x + 2);
        Instantiate(thunder, new Vector3(posx, transform.position.y + 7.9f, 0), Quaternion.identity);
    }

    private void FlipSprite()
    {
        if (_player.transform.position.x - transform.position.x > 0)
        {
            _renderer.flipX = true;
        }
        else
        {
            _renderer.flipX = false;
        }
    }

    public IEnumerator DisableShield(float countDown)
    {
        yield return new WaitForSeconds(countDown);
        transform.GetChild(0).gameObject.SetActive(false);
    }
    
    // Damage
    public void BossDamage(int damage)
    {
        bossHP -= damage;
        if(bossHP < 0) Die();
    }
    
    public IEnumerator LoadVictory(float countDown)
    {
        yield return new WaitForSeconds(countDown);
        SceneManager.LoadScene("victory");
    }
    
    public void Die()
    { 
        _animator.SetTrigger("die");
        StartCoroutine(LoadVictory(0.8f));
        
    }

    public int BossGetHP()
    {
        return bossHP;
    }
    
}
