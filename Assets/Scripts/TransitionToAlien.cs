using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TransitionToAlien : MonoBehaviour
{
    [SerializeField] float timeTillStart = 2700f;
    [SerializeField] float maxAlpha = 1.0f;
    [SerializeField] float minAlpha = 0.0f;
    [SerializeField] float time = 4.0f;
    [SerializeField] Image white;
    [SerializeField] float timeTest = 0.0f;
    [SerializeField] int alienIndex;
    [SerializeField] AudioClip ufo;
    private AudioSource audioSource;
    private bool inProgress = false;
    // Start is called before the first frame update
    void Start()
    {
        white.canvasRenderer.SetAlpha(minAlpha);
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeSinceLevelLoad == timeTillStart || timeTest == timeTillStart)
        {
            if(!inProgress)
            {
                transitionToWhite();
                audioSource.PlayOneShot(ufo);
                Invoke("LoadAlienEnding", time + 2.0f);
                inProgress = true;
            }
        }
    }

    private void transitionToWhite()
    {
        white.canvasRenderer.SetAlpha(minAlpha);
        white.CrossFadeAlpha(maxAlpha, time, false);
    }

    private void LoadAlienEnding()
    {
        SceneManager.LoadScene(alienIndex);
    }
}
