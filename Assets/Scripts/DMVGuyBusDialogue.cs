using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DMVGuyBusDialogue : MonoBehaviour
{
    [SerializeField] Canvas question;
    [SerializeField] Canvas responseNice;
    [SerializeField] Canvas responseMean;
    private GameObject player;
    private Transform playerTransform;
    private PlayerLook lookingScript;
    private PlayerWalking walkingScript;
    private bool isInRange = false;
    private bool isPressed = false;
    private bool reenableWalking = false;
    private bool isLookAt = false;
    private bool hasInteracted = false;
    private Quaternion initialPlayer;
    private Quaternion initialNPC;
    private Quaternion targetRotationNPC;
    [SerializeField] int speed = 5;
    public int choice = 0;

    // Start is called before the first frame update
    void Start()
    {
        question.enabled = false;
        responseMean.enabled = false;
        responseNice.enabled = false;

        player = GameObject.Find("MainPlayer");
        playerTransform = player.GetComponent<Transform>();
        walkingScript = player.GetComponent<PlayerWalking>();

        lookingScript = GameObject.Find("Camera").GetComponent<PlayerLook>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isInRange && !isPressed)
        {
            question.enabled = true;

            if (walkingScript.enabled)
            {
                reenableWalking = true;
                walkingScript.enabled = false;
            }

            lookingScript.enabled = false;

            Cursor.lockState = CursorLockMode.None;
            if (!isLookAt)
            {
                initialPlayer = player.GetComponent<Transform>().rotation;
                initialNPC = transform.rotation;
               // var targetRotationPlayer = Quaternion.LookRotation(transform.position - player.transform.position);
               // targetRotationNPC = Quaternion.LookRotation(player.transform.position - transform.position);
               // player.transform.rotation = Quaternion.Slerp(player.transform.rotation, targetRotationPlayer, speed * Time.deltaTime);
              //  transform.rotation = Quaternion.Slerp(transform.rotation, targetRotationNPC, speed * Time.deltaTime);
                playerTransform.LookAt(gameObject.transform);
                transform.LookAt(playerTransform);
                isLookAt = true;
            } 
        }

        else if ((Input.GetKey(KeyCode.E) || Input.GetKey(KeyCode.Mouse0)) && isPressed && !hasInteracted)
        {
            responseMean.enabled = false;
            responseNice.enabled = false;
            lookingScript.enabled = true;
            if (reenableWalking)
            {
                walkingScript.enabled = true;
            }
            playerTransform.SetPositionAndRotation(playerTransform.position, initialPlayer);
            transform.SetPositionAndRotation(transform.position, initialNPC);
            hasInteracted = true;
        }
    }

    private void OnTriggerEnter(Collider collide)
    {
        if (collide.tag == "Player") //set to whatever triggers the interaction
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

    public void button1Pressed()
    {
        choice = 1;
        Cursor.lockState = CursorLockMode.Locked;
        question.enabled = false;
        responseNice.enabled = true;
        isPressed = true;
    }
    public void button2Pressed()
    {
        choice = 2;
        Cursor.lockState = CursorLockMode.Locked;
        question.enabled = false;
        responseNice.enabled = true;
        isPressed = true;
    }
    public void button3Pressed()
    {
        choice = 3;
        Cursor.lockState = CursorLockMode.Locked;
        question.enabled = false;
        responseMean.enabled = true;
        isPressed = true;
    }
}
