using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerChooseOption : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject question;
    [SerializeField] GameObject answerOne;
    [SerializeField] GameObject answerTwo;
    [SerializeField] GameObject answerThree;

    private bool isInRange = false;

    void Start()
    {
        
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
    // Update is called once per frame
    void Update()
    {
        if (isInRange)
        {

        }
    }
}
