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
            Cursor.lockState = CursorLockMode.Confined;
            GameObject.Find("MainPlayer").GetComponent<PlayerWalking>().enabled = false;
            GameObject.Find("Camera").GetComponent<PlayerLook>().enabled = false;
            playerTransform.LookAt(gameObject.transform);
            transform.LookAt(playerTransform);
            GameObject.Find("Canvas").GetComponent<Canvas>().enabled = true;
            if (Input.GetKey(KeyCode.E))
            {
                GameObject.Find("Canvas").GetComponent<Canvas>().enabled = false;
                playerTransform.SetPositionAndRotation(playerTransform.position, new Quaternion(0,0,0,0));
                hasInteracted = true;
            }
        }
        else
        {
            //Cursor.lockState = CursorLockMode.Locked;
            GameObject.Find("MainPlayer").GetComponent<PlayerWalking>().enabled = true;
            GameObject.Find("Camera").GetComponent<PlayerLook>().enabled = true;
        }
    }
}
