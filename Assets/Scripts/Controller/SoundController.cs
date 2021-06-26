using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour
{
    private AudioSource bulletSound;
    private AudioSource jumpSound;
    void Start()
    {
        bulletSound = transform.GetChild(1).GetComponent<AudioSource>();
        jumpSound = transform.GetChild(2).GetComponent<AudioSource>();
    }

    public void BulletSound()
    {
        bulletSound.Play();
    }
    
    public void JumpSound()
    {
        jumpSound.Play();
    }
}
