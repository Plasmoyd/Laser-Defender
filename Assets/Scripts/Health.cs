using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] bool isPlayer;
    [SerializeField] int health;
    [SerializeField] int score = 50;
    [SerializeField] ParticleSystem onHitEffect;
    [SerializeField] bool applyCameraShake;

    AudioPlayer audioPlayer;

    CameraShake cameraShake;

    ScoreKeeper scoreKeeper;

    LevelManager levelManager;

    private void Awake() 
    {
        audioPlayer = FindObjectOfType<AudioPlayer>();
        cameraShake = Camera.main.GetComponent<CameraShake>();   
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
        levelManager = FindObjectOfType<LevelManager>();
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        DamageDealer damageDealer = other.GetComponent<DamageDealer>();

        if(damageDealer != null)
        {
            TakeDamage(damageDealer.GetDamage());
            PlayHitEffect();
            PlayExplosionsSound();
            ShakeCamera();
            damageDealer.Hit();
        }

        

    }

    private void TakeDamage(int damage)
    {
        health -= damage;
        if(health <= 0)
        {
            Die();
        }
    }

    private void Die() 
    {
        if(!isPlayer)
        {
            scoreKeeper.ModifyCurrentScore(score);
        }
        
        Destroy(gameObject);

        if(isPlayer && levelManager != null)
        {
            levelManager.LoadGameOver();
        }
    }

    private void PlayHitEffect()
    {

        if(onHitEffect != null)
        {
            ParticleSystem instance = Instantiate(onHitEffect, transform.position, Quaternion.identity);
            Destroy(instance.gameObject, instance.main.duration + instance.main.startLifetime.constantMax);
        }
    }

    private void PlayExplosionsSound()
    {
        if(audioPlayer != null)
        {
            audioPlayer.PlayExplosionsClip();
        }
    }

    private void ShakeCamera()
    {
        if(cameraShake != null && applyCameraShake)
        {
            cameraShake.Play();
        }
    }

    public int GetHealth()
    {
        return health;
    }
}
