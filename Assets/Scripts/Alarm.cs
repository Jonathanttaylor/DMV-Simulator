using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alarm : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;
    private bool isOn = true;
    private bool isInRange = false;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        TurnOffAlarm();
    }

    private void TurnOffAlarm()
    {
        if (isInRange && isOn && Input.GetKey(KeyCode.E))
        {
            audioSource.Stop();
        }
    }

    private void OnTriggerEnter(Collider collide)
    {
        if (collide.tag == "Player")
        {
            isInRange = true;
        }
    }

    private void OnTriggerExit(Collider collide)
    {
        if (collide.tag == "Player")
        {
            isInRange = false;
        }
    }
}
