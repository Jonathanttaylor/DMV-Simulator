using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DMVGuyBusDialogue1 : MonoBehaviour
{
    [SerializeField] Canvas question;
    [SerializeField] Canvas responseNice;
    [SerializeField] Canvas responseMean;
    private GameObject player;
    private GameObject playerCamera;
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
    [SerializeField] GameObject bus;
    private BusMovement busScript;
    [SerializeField] int wavepoint = 21;
    [SerializeField] GameObject chairSitting;
    private BusSitting chairSittingScript;
    private AudioSource audio;
    [SerializeField] AudioClip questionAudio;
    [SerializeField] AudioClip responseNiceAudio;
    [SerializeField] AudioClip responseMeanAudio;
    private bool isQuestion = false;
    private bool isResponseNice = false;
    private bool isResponseMean = false;

    // Start is called before the first frame update
    void Start()
    {
        question.enabled = false;
        responseMean.enabled = false;
        responseNice.enabled = false;

        player = GameObject.Find("MainPlayer");
        playerTransform = player.GetComponent<Transform>();
        walkingScript = player.GetComponent<PlayerWalking>();

        playerCamera = GameObject.Find("Camera");
        lookingScript = playerCamera.GetComponent<PlayerLook>();
        busScript = bus.GetComponent<BusMovement>();
        chairSittingScript = chairSitting.GetComponent<BusSitting>();
        audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if ((busScript.ReturnCurrentWaypoint() == wavepoint) && !isPressed)
        {
            question.enabled = true;
            if (!isQuestion)
            {
                audio.PlayOneShot(questionAudio);
                isQuestion = true;
            }
            
            /*
                        if (walkingScript.enabled)
                        {
                            reenableWalking = true;
                            walkingScript.enabled = false;
                        }
                        */
            //lookingScript.enabled = false;
            chairSittingScript.disableLook = true;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            if (!isLookAt)
            {
                initialPlayer = player.GetComponent<Transform>().rotation;
                initialNPC = transform.rotation;
                // var targetRotationPlayer = Quaternion.LookRotation(transform.position - player.transform.position);
                // targetRotationNPC = Quaternion.LookRotation(player.transform.position - transform.position);
                // player.transform.rotation = Quaternion.Slerp(player.transform.rotation, targetRotationPlayer, speed * Time.deltaTime);
                //  transform.rotation = Quaternion.Slerp(transform.rotation, targetRotationNPC, speed * Time.deltaTime);
                playerCamera.transform.LookAt(transform);
                transform.Rotate(-25, 0, 0);
                isLookAt = true;
            }
        }

        else if ((Input.GetKey(KeyCode.E) || Input.GetKey(KeyCode.Mouse0)) && isPressed && !hasInteracted)
        {
            responseMean.enabled = false;
            responseNice.enabled = false;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            //  lookingScript.enabled = true;
            if (reenableWalking)
            {
                walkingScript.enabled = true;
            }
            playerTransform.SetPositionAndRotation(playerTransform.position, initialPlayer);
            transform.SetPositionAndRotation(transform.position, initialNPC);
            hasInteracted = true;
            chairSittingScript.disableLook = false;
        }
    }
    /*
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
    */
    public void button1Pressed()
    {
        audio.Stop();
        choice = 1;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        question.enabled = false;
        if (!isResponseMean)
        {
            audio.Stop();
            audio.PlayOneShot(responseMeanAudio);
            isResponseMean = true;
        }
        responseMean.enabled = true;
        isPressed = true;
    }
    public void button2Pressed()
    {
        audio.Stop();
        choice = 2;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        question.enabled = false;
        if (!isResponseNice)
        {
            audio.Stop();
            audio.PlayOneShot(responseNiceAudio);
            isResponseNice = true;
        }
        responseNice.enabled = true;
        isPressed = true;
    }

    public bool ReturnIsPressed()
    {
        return isPressed;
    }
}
