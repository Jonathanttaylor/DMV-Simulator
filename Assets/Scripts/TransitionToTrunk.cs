using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TransitionToTrunk : MonoBehaviour
{
    [SerializeField] GameObject computerFace;
    private DMVInteraction interaction;
    [SerializeField] Image Black;
    [SerializeField] float delay = 1.0f;
    private AudioSource audioSource;
    [SerializeField] AudioClip gettingHit;
    [SerializeField] int trunkIndex;
    private bool isInRange;
    private bool isPlaying = false;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        Black.canvasRenderer.SetAlpha(0.0f);
    }

    // Update is called once per frame
    void Update()
    {
        if (isInRange)
        {
            Black.canvasRenderer.SetAlpha(1.0f);
            if (!isPlaying)
            {
                audioSource.PlayOneShot(gettingHit);
                isPlaying = true;
            }
            Invoke("loadTrunkEnding", delay);
        }
    }

    private void loadTrunkEnding()
    {
        SceneManager.LoadScene(trunkIndex);
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
