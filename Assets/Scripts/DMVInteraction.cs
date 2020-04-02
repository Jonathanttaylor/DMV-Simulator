using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DMVInteraction : MonoBehaviour
{
    private GameObject player;
    public int choice;
    [SerializeField] Canvas needNumber;
    [SerializeField] Canvas kidnap;
    [SerializeField] Canvas toolate;
    [SerializeField] Image red;
    [SerializeField] float maxAlpha = 1.0f;
    [SerializeField] float minAlpha = 0.0f;
    [SerializeField] float time = 5.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void fadeToRed()
    {
        red.canvasRenderer.SetAlpha(minAlpha);
        red.CrossFadeAlpha(maxAlpha, time, false);
    }
}
