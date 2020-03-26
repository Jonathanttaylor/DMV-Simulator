using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
   public void PlayGame()
    {
        SceneManager.LoadScene(1);
        SceneManager.LoadScene(2, LoadSceneMode.Additive);
        SceneManager.LoadScene(3, LoadSceneMode.Additive);
        SceneManager.LoadScene(4, LoadSceneMode.Additive);
        SceneManager.LoadScene(5, LoadSceneMode.Additive);
        SceneManager.LoadScene(6, LoadSceneMode.Additive);
        SceneManager.LoadScene(7, LoadSceneMode.Additive);
    }

    public void QuitGame()
    {
        Debug.Log("QUIT");
        Application.Quit();
    }
}
