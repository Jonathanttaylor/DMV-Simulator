using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BusAnnoying : MonoBehaviour
{
    [SerializeField] AudioClip intro;
    [SerializeField] AudioClip convo1;
    [SerializeField] AudioClip convo2;
    [SerializeField] AudioClip convo3;
    [SerializeField] AudioClip convo4;
    private bool isPlaying1 = false;
    private bool isPlaying2 = false;
    private bool isPlaying3 = false;
    private bool isPlaying4 = false;
    private bool isPlaying5 = false;
    private AudioSource audioSource;
    [SerializeField] GameObject bus;
    private BusMovement busScript;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        busScript = bus.GetComponent<BusMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (busScript.ReturnCurrentWaypoint() == 18)
        {
            audioSource.Stop();
            if (!isPlaying1)
            {
                audioSource.PlayOneShot(intro);
                isPlaying1 = true;
            }  
        }
        else if (busScript.ReturnCurrentWaypoint() == 30)
        {
            audioSource.Stop();
            if (!isPlaying2)
            {
                audioSource.PlayOneShot(convo1);
                isPlaying2 = true;
            }
        }
        else if (busScript.ReturnCurrentWaypoint() == 40)
        {
            audioSource.Stop();
            if (!isPlaying3)
            {
                audioSource.PlayOneShot(convo2);
                isPlaying3 = true;
            }
        }
        else if (busScript.ReturnCurrentWaypoint() == 50)
        {
            audioSource.Stop();
            if (!isPlaying4)
            {
                audioSource.PlayOneShot(convo3);
                isPlaying4 = true;
            }
        }
        else if (busScript.ReturnCurrentWaypoint() == 50)
        {
            audioSource.Stop();
            if (!isPlaying5)
            {
                audioSource.PlayOneShot(convo4);
                isPlaying5 = true;
            }
        }

    }
}
