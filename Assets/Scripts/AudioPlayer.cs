using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    [Header("Shooting")]
    [SerializeField] AudioClip shootingClip;
    [SerializeField] [Range(0f, 1f)] float shootingVolume = 1f;

    [Header("Explosions")]
    [SerializeField] AudioClip explosionsClip;
    [SerializeField] [Range(0f, 1f)] float explosionsVolume = 0.5f;

    public void PlayShootingClip()
    {
        PlayClip(shootingClip, shootingVolume);
    }

    public void PlayExplosionsClip()
    {
        PlayClip(explosionsClip, explosionsVolume);
    }

    private void PlayClip(AudioClip audioClip, float volume)
    {

        Vector3 position = Camera.main.transform.position;

        if(audioClip != null)
        {
            AudioSource.PlayClipAtPoint(audioClip, position, volume);
        }
    }
}
