using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSounds : MonoBehaviour
{
    public AudioSource source;
    public AudioClip engineRev;
    public ParticleSystem exhaust;
    bool playing;


    // Start is called before the first frame update
    void Start()
    {
        TestingEvents.current.forwardMovement += OnAccelerateForward;
    }

    private void OnAccelerateForward()
    {
        //source.PlayOneShot(engineRev);
        if (playing == false)
        {
            
            StartCoroutine(PlayRevNoise());
        }
        exhaust.Play(); ;
    }
    // Update is called once per frame
    void Update()
    {
        if(!playing)
        {
            exhaust.Stop();
        }
    }

    IEnumerator PlayRevNoise()
    {
        
        playing = true;


        source.PlayOneShot(engineRev);
        Debug.Log("got here");

        yield return new WaitForSeconds(engineRev.length-0.5f);
        playing = false;
        
    }
}
