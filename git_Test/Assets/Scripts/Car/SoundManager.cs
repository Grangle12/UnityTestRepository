using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource source;
    public AudioClip engineRev;
    public AudioClip coinPickup;

    bool playing;
    public bool enableSound, engineSound;

    // Start is called before the first frame update
    void Start()
    {
        EventManager.current.forwardMovement += Play_AccelerationSound;
    }

    private void Play_AccelerationSound()
    {

        if (playing == false && enableSound && engineSound)
        {
            
            StartCoroutine(PlayRevNoise());
        }


    }

    public void PlayCoinPickupSound()
    {
        if(enableSound)
        {
            StartCoroutine(CoinPickupSound());
        }
    }


    IEnumerator PlayRevNoise()
    {
        
        playing = true;
        source.PlayOneShot(engineRev);

        yield return new WaitForSeconds(engineRev.length-0.5f);

        playing = false;
        
    }

    IEnumerator CoinPickupSound()
    {

        playing = true;
        source.PlayOneShot(coinPickup);

        yield return new WaitForSeconds(coinPickup.length - 0.5f);

        playing = false;

    }
}
