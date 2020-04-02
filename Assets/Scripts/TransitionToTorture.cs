using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TransitionToTorture : MonoBehaviour
{
    [SerializeField] GameObject computerFace;
    private DMVInteraction interaction;
    [SerializeField] float maxAlpha = 1.0f;
    [SerializeField] float minAlpha = 0.0f;
    private AudioSource audioSource;
    [SerializeField] Image black;
    [SerializeField] Image red;
    [SerializeField] float time = 0.5f;
    [SerializeField] AudioClip calmDown;
    private bool inProgress = false;
    [SerializeField] int tortureIndex;
    // Start is called before the first frame update
    void Start()
    {
        red.canvasRenderer.SetAlpha(minAlpha);
         black.canvasRenderer.SetAlpha(minAlpha);
        interaction = computerFace.GetComponent<DMVInteraction>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (interaction.startTortureEnding && interaction.endCanvasClosed)
        {
            if (!inProgress)
            {
                fadeToRed();
                Invoke("setBlack", time + 0.001f);
                audioSource.PlayOneShot(calmDown);
                Invoke("LoadTortureScene", time + 2.0f);
                inProgress = true;
            }
        }
    }
    private void fadeToRed()
    {
        red.canvasRenderer.SetAlpha(minAlpha);
        red.CrossFadeAlpha(maxAlpha, time, false); 
    }

    private void setBlack()
    {
        red.canvasRenderer.SetAlpha(0.0f);
        black.canvasRenderer.SetAlpha(1.0f);
    }

    private void LoadTortureScene()
    {
        SceneManager.LoadScene(tortureIndex);
    }
}
