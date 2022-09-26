using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{

    [Header("Projectiles")]
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] float projectileSpeed = 10f;
    [SerializeField] float projectileLifetime = 5f;

    [Header("Fire rate")]
    [SerializeField] float fireRate = 0.5f;
    [SerializeField] float fireRateVariance = 0f;
    [SerializeField] float minFireRate = 0.2f;

    [Header("AI")]
    [SerializeField] bool isUsingAI;

    [HideInInspector] public bool isFiring;

    Coroutine firingCoroutine;

    AudioPlayer audioPlayer;

    private void Awake() 
    {
        audioPlayer = FindObjectOfType<AudioPlayer>();    
    }

    void Start()
    {
        if(isUsingAI)
        {
            isFiring = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Fire();
    }

    private void Fire()
    {
        if(isFiring && firingCoroutine == null)
        {
           firingCoroutine =  StartCoroutine(FireContinuosly());
        }
        else if(!isFiring && firingCoroutine != null)
        {
            StopCoroutine(firingCoroutine);
            firingCoroutine = null;
        }
    }

    IEnumerator FireContinuosly()
    {
        while(true)
        {
            GameObject projectile = Instantiate(projectilePrefab, transform.position, transform.rotation);

            Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
            if(!isUsingAI)
            {
                rb.velocity = transform.up * projectileSpeed;
            }
            else
            {
                rb.velocity = -transform.up * projectileSpeed;
            }
            

            Destroy(projectile, projectileLifetime);

            PlayShootingAudio();

            yield return new WaitForSecondsRealtime(GetRandomFireRate());
        }
    }

    private float GetRandomFireRate()
    {
        float randomFireRate =  Random.Range(fireRate - fireRateVariance, fireRate + fireRateVariance);
        return Mathf.Clamp(randomFireRate, minFireRate, float.MaxValue);
    }

    private void PlayShootingAudio()
    {
        if(audioPlayer != null)
        {
            audioPlayer.PlayShootingClip();
        }
    }
}
