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
    private bool isPlaying1;
    private bool isPlaying2;
    private bool isPlaying3;
    private bool isPlaying4;
    private bool isPlaying5;
    private AudioSource audio;
    [SerializeField] GameObject bus;
    private BusMovement busScript;
    // Start is called before the first frame update
    void Start()
    {
        audio = GetComponent<AudioSource>();
        busScript = bus.GetComponent<BusMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (busScript.ReturnCurrentWaypoint() == 18)
        {
            audio.Stop();
            if (!isPlaying1)
            {
                audio.PlayOneShot(intro);
                isPlaying1= true;
            }  
        }
        else if (busScript.ReturnCurrentWaypoint() == 30)
        {
            audio.Stop();
            if (!isPlaying2)
            {
                audio.PlayOneShot(convo1);
                isPlaying2 = true;
            }
        }
        else if (busScript.ReturnCurrentWaypoint() == 40)
        {
            audio.Stop();
            if (!isPlaying3)
            {
                audio.PlayOneShot(convo2);
                isPlaying3 = true;
            }
        }
        else if (busScript.ReturnCurrentWaypoint() == 50)
        {
            audio.Stop();
            if (!isPlaying4)
            {
                audio.PlayOneShot(convo3);
                isPlaying4 = true;
            }
        }
        else if (busScript.ReturnCurrentWaypoint() == 50)
        {
            audio.Stop();
            if (!isPlaying5)
            {
                audio.PlayOneShot(convo4);
                isPlaying5 = true;
            }
        }

    }
}
