using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    private Vector2 velocity;
    [SerializeField] private float shotSpeed;
    private Player _player;
    private Vector2 distance;

    

    private Rigidbody2D _rigidbody2D;
    // Start is called before the first frame update
    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _player = FindObjectOfType<Player>();
        setVelocity();
        Destroy(gameObject, 5f);
    }

    private void setVelocity()
    {
        Vector3 normalizedDistance = (_player.transform.position - transform.position).normalized;
        _rigidbody2D.velocity = normalizedDistance * shotSpeed;
    }

}
