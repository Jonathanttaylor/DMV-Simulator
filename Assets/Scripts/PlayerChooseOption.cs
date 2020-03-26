using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerChooseOption : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject question;
    [SerializeField] GameObject answerOne;
    [SerializeField] GameObject answerTwo;
    [SerializeField] GameObject answerThree;
    private Transform playerTransform;
    private bool hasInteracted = false;
    private bool isInRange = false;
    private bool fixedView = false;
    private bool isPressed = false;

    void Start()
    {
        playerTransform = GameObject.Find("MainPlayer").GetComponent<Transform>();
        GameObject.Find("Canvas").GetComponent<Canvas>().enabled = false;
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
        if (isInRange && !hasInteracted)
        {
            Cursor.lockState = CursorLockMode.None;
            GameObject.Find("MainPlayer").GetComponent<PlayerWalking>().enabled = false;
            GameObject.Find("Camera").GetComponent<PlayerLook>().enabled = false;
            playerTransform.LookAt(gameObject.transform);
            transform.LookAt(playerTransform);
            GameObject.Find("Canvas").GetComponent<Canvas>().enabled = true;
        }
        if (!fixedView && hasInteracted)
        {
            Cursor.lockState = CursorLockMode.Locked;
            fixedView = true;
            GameObject.Find("MainPlayer").GetComponent<PlayerWalking>().enabled = true;
            GameObject.Find("Camera").GetComponent<PlayerLook>().enabled = true;
        }
    }

    public void buttonPressed()
    {
        Debug.Log("Button Pressed!");
        isPressed = true;
        GameObject.Find("Canvas").GetComponent<Canvas>().enabled = false;
        playerTransform.SetPositionAndRotation(playerTransform.position, new Quaternion(0, 0, 0, 0));
        hasInteracted = true;
    }
}
