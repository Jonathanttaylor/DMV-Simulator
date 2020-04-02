using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TrunkEnding : MonoBehaviour
{
    [SerializeField] Image Black;
    [SerializeField] float time = 5.0f;
    [SerializeField] float whenToFade = 13.0f;
    [SerializeField] float whenCarStarts = 1.0f;
    [SerializeField] float whenTrunkCloses = 1.0f;
    [SerializeField] float maxAlpha = 1.0f;
    [SerializeField] float minAlpha = 0.0f;
    [SerializeField] AudioClip trunkClosing;
    [SerializeField] AudioClip carStarting;
    private AudioSource audioSource;
    private bool playedCar = false;
    private bool playedTrunk = false;
    // Start is called before the first frame update
    void Start()
    {
        Black.canvasRenderer.SetAlpha(maxAlpha);
        fadeFromBlack();
        Invoke("fadeToBlack", whenToFade);
        audioSource = GetComponent<AudioSource>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Black.canvasRenderer.GetAlpha() == maxAlpha)
        {
            if(!playedTrunk)
            {
                Invoke("playTrunk", whenTrunkCloses);
                playedTrunk = true;
            }
            if (!playedCar)
            {
                Invoke("playCar", whenCarStarts);
                playedCar = true;
            }
        }
        Invoke("goToMain", 25.0f);
    }

    private void fadeToBlack()
    {
        Black.canvasRenderer.SetAlpha(minAlpha);
        Black.CrossFadeAlpha(maxAlpha, time, false);
    }

    private void fadeFromBlack()
    {
        Black.canvasRenderer.SetAlpha(maxAlpha);
        Black.CrossFadeAlpha(minAlpha, time, false);
    }

    private void playCar()
    {
        audioSource.PlayOneShot(carStarting);
    }

    private void playTrunk()
    {
        audioSource.PlayOneShot(trunkClosing);
    }

    private void goToMain()
    {
        SceneManager.LoadScene(0);
    }
}
