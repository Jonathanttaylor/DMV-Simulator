using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class AlienEnding : MonoBehaviour
{
    [SerializeField] Image Black;
    [SerializeField] float time = 5.0f;
    [SerializeField] int numOfFades = 3;
    [SerializeField] float holdBlackTime = 0.5f;
    [SerializeField] float maxAlpha = 1.0f;
    [SerializeField] float minAlpha = 0.0f;
    [SerializeField] GameObject dmvGuy;
    [SerializeField] GameObject dmvGuyAlien;
    [SerializeField] GameObject dmvGuyScrewdriver;
    [SerializeField] GameObject alienFriend1;
    [SerializeField] GameObject alienFriend2;
    [SerializeField] GameObject screwDriver;
    private AudioSource audioSource;
    private int count = 0;

    // Start is called before the first frame update
    void Start()
    {
        dmvGuy.GetComponent<Renderer>().enabled = true;
        dmvGuyAlien.GetComponent<SkinnedMeshRenderer>().enabled = false;
        dmvGuyScrewdriver.GetComponent<SkinnedMeshRenderer>().enabled = false;
        screwDriver.GetComponent<Renderer>().enabled = false;
        alienFriend1.GetComponent<SkinnedMeshRenderer>().enabled = false;
        alienFriend2.GetComponent<SkinnedMeshRenderer>().enabled = false;
        audioSource = GetComponent<AudioSource>();
        audioSource.Play();
        Black.canvasRenderer.SetAlpha(maxAlpha);
        fadeFromBlack();
    }

    // Update is called once per frame
    void Update()
    {
        if (count != numOfFades)
        {

            if (Black.canvasRenderer.GetAlpha() == maxAlpha)
            {
                Invoke("fadeFromBlack", holdBlackTime);
                switchCharacters();
            }
            else if (Black.canvasRenderer.GetAlpha() == minAlpha)
            {
                fadeToBlack();
                count++;
            }
        }
        Invoke("goToMain", 25.0f);
    }

    private void switchCharacters()
    {
        if (count == 1)
        {
            dmvGuy.GetComponent<Renderer>().enabled = false;
            dmvGuyAlien.GetComponent<SkinnedMeshRenderer>().enabled = true;
        }
        if (count == 2)
        {
            dmvGuyAlien.GetComponent<SkinnedMeshRenderer>().enabled = false;
            dmvGuyScrewdriver.GetComponent<SkinnedMeshRenderer>().enabled = true;
            screwDriver.GetComponent<Renderer>().enabled = true;
            alienFriend1.GetComponent<SkinnedMeshRenderer>().enabled = true;
            alienFriend2.GetComponent<SkinnedMeshRenderer>().enabled = true;
        }
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

    private void goToMain()
    {
        SceneManager.LoadScene(0);
    }
}
