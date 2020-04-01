using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAmy : MonoBehaviour
{
    [SerializeField] AudioClip badComedians;
    private AudioSource audioSource;
    private bool isPlaying = false;
    public bool start = false;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        playAmy();
    }

    private void playAmy()
    {
        if (!isPlaying && start)
        {
            audioSource.PlayOneShot(badComedians);
            isPlaying = true;
        }
    }
}
