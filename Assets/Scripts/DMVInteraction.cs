using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DMVInteraction : MonoBehaviour
{
    private GameObject player;
    private GameObject playerCamera;
    private PlayerWalking walkingScript;
    private PlayerLook lookingScript;
    public int choice;
    private bool isInRange = false;
    private bool hasToldGetNum = false;
    private Quaternion initialPlayer;
    private Quaternion initialComputer;
    [SerializeField] Canvas needNumber;
    [SerializeField] Canvas helpAroundBack;
    [SerializeField] Canvas tooLate;
    [SerializeField] Canvas nothingICanDo;
    [SerializeField] GameObject TakeANumber;
    [SerializeField] GameObject nowServingNum;
    private GameObject choices;
    private NowServingNumber nowServingNumScript;
    private PickANumber takeANumberScript;
    [SerializeField] int ticketNumber;
    [SerializeField] int servingNumber;
    public bool isTrunkEnding = false;
    public bool startTortureEnding = false;
    private bool refused = false;
    private bool tookAnother = false;
    public int total = 0;
    private bool hadSecondInteract = false;
    public bool allChoicesMade = false;
    public bool endCanvasClosed = false;
    private bool hadFirstInteract = false;
    private bool alreadyEnabled = false;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("MainPlayer");
        playerCamera = GameObject.Find("Camera");
        walkingScript = player.GetComponent<PlayerWalking>();
        lookingScript = playerCamera.GetComponent<PlayerLook>();
        needNumber.enabled = false;
        helpAroundBack.enabled = false;
        tooLate.enabled = false;
        nothingICanDo.enabled = false;
        //red.canvasRenderer.SetAlpha(0.0f);
        takeANumberScript = TakeANumber.GetComponent<PickANumber>();
        nowServingNumScript = nowServingNum.GetComponent<NowServingNumber>();
        choices = GameObject.FindGameObjectWithTag("Choices");
    }

    // Update is called once per frame
    void Update()
    {
        int sum = choices.GetComponent<DMVGuyBusDialogue>().choice + choices.GetComponent<DMVGuyBusDialogue1>().choice + choice;
        total = sum;

        ticketNumber = takeANumberScript.ticketNumber;
        servingNumber = nowServingNumScript.number;

        if (isInRange)
        {
            goTakeANum();

            hasNum();
        }

        if ((Input.GetKey(KeyCode.E) || Input.GetKey(KeyCode.Mouse0)) && (helpAroundBack.enabled || nothingICanDo.enabled))
        {
            endCanvasClosed = true;
            helpAroundBack.enabled = false;
            nothingICanDo.enabled = false;
        }

        if (walkingScript.enabled)
        {
            helpAroundBack.enabled = false;
            nothingICanDo.enabled = false;
        }

        if (allChoicesMade)
        {
            if (choices.GetComponent<DMVGuyBusDialogue>().choice + choices.GetComponent<DMVGuyBusDialogue1>().choice + choice < 4)
            {
                if (!alreadyEnabled)
                {
                    helpAroundBack.enabled = true;
                    alreadyEnabled = true;
                }
                isTrunkEnding = true;
            }
            else
            {
                if (!alreadyEnabled)
                {
                    nothingICanDo.enabled = true;
                    alreadyEnabled = true;
                }
                startTortureEnding = true;
            }
        }
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

    private void goTakeANum()
    {
        if (!hasToldGetNum && (takeANumberScript.hasTakenNumber() == false))
        {
            StartingInteract();
            needNumber.enabled = true;
            hasToldGetNum = true;
        }
        if (Input.GetKey(KeyCode.E) || Input.GetKey(KeyCode.Mouse0) && needNumber.enabled)
        {
            hadFirstInteract = true;
            EndInteract();
        }
    }

    private void hasNum()
    {
        if (servingNumber >= ticketNumber && ticketNumber != 0 && !hadSecondInteract)
        {
            hadSecondInteract = true;
            StartingInteract();
            Cursor.lockState = CursorLockMode.None;
            tooLate.enabled = true;
        }
    }

    public void refuse()
    {
        choice = 1;
        allChoicesMade = true;
        tooLate.enabled = false;
      //  pickEnding();
    }

    public void takeAnother()
    {
        choice = 2;
        allChoicesMade = true;
        tooLate.enabled = false;
        // pickEnding();
    }


    private void StartingInteract()
    {
        walkingScript.enabled = false;
        lookingScript.enabled = false;
        initialPlayer = player.transform.rotation;
        initialComputer = transform.rotation;
        transform.LookAt(player.transform);
        player.transform.LookAt(transform);
    }

    private void EndInteract()
    {
        needNumber.enabled = false;
        helpAroundBack.enabled = false;
        tooLate.enabled = false;
        nothingICanDo.enabled = false;
        player.transform.rotation = initialPlayer;
        transform.rotation = initialComputer;
        walkingScript.enabled = true;
        lookingScript.enabled = true;
        Cursor.lockState = CursorLockMode.Locked;
    }
    
    private void pickEnding()
    {
        if (choices.GetComponent<DMVGuyBusDialogue>().choice + choices.GetComponent<DMVGuyBusDialogue1>().choice + choice < 4)
        {
            helpAroundBack.enabled = true;
            isTrunkEnding = true;
        }
        else
        {
            nothingICanDo.enabled = true;
            startTortureEnding = true;
        }
    }
}
