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
    private bool hadSecondInteract = false;
    private bool hadFirstInteract = false;
    private bool hasToldGetNum = false;
    private Quaternion initialPlayer;
    private Quaternion initialComputer;
    [SerializeField] Canvas needNumber;
    [SerializeField] Canvas helpAroundBack;
    [SerializeField] Canvas tooLate;
    [SerializeField] Canvas tooLateAgain;
    [SerializeField] Canvas nothingICanDo;
    [SerializeField] Image red;
    [SerializeField] float maxAlpha = 1.0f;
    [SerializeField] float minAlpha = 0.0f;
    [SerializeField] float time = 5.0f;
    [SerializeField] GameObject TakeANumber;
    [SerializeField] GameObject nowServingNum;
    private GameObject choices;
    private NowServingNumber nowServingNumScript;
    private PickANumber takeANumberScript;
    [SerializeField] int trunkIndex;
    [SerializeField] int tortureIndex;
    [SerializeField] int ticketNumber = 1000;
    [SerializeField] int servingNumber = 0;
    public bool restart = false;
    public bool isTrunkEnding = false;
    public bool isTortureEnding = false;

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
        tooLateAgain.enabled = false;
        nothingICanDo.enabled = false;
        red.canvasRenderer.SetAlpha(0.0f);
        takeANumberScript = TakeANumber.GetComponent<PickANumber>();
        nowServingNumScript = nowServingNum.GetComponent<NowServingNumber>();
        choices = GameObject.FindGameObjectWithTag("Choices");
    }

    // Update is called once per frame
    void Update()
    {
     //   int number = nowServingNumScript.number;
        servingNumber = takeANumberScript.nowserving.number;
        ticketNumber = takeANumberScript.ticketNumber;

        if (isInRange)
        {
            goTakeANum();
            hasNum();
        }

        if ((Input.GetKey(KeyCode.E) || Input.GetKey(KeyCode.Mouse0)) && tooLate.enabled)
        {
            EndInteract();
        }
    }

    private void fadeToRed()
    {
        red.canvasRenderer.SetAlpha(minAlpha);
        red.CrossFadeAlpha(maxAlpha, time, false);
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
        if (Input.GetKey(KeyCode.E) || Input.GetKey(KeyCode.Mouse0))
        {
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
        tooLate.enabled = false;
        int sum = choices.GetComponent<DMVGuyBusDialogue>().choice + choices.GetComponent<DMVGuyBusDialogue1>().choice + choice;
        if (sum < 4)
        {
            helpAroundBack.enabled = true;
            isTrunkEnding = true;
        }
        else
        {
            isTortureEnding = true;
        }
    }

    public void takeAnother()
    {
        tooLate.enabled = false;
        choice = 2;
        EndInteract();
        takeANumberScript.setDisplayTicket();
    }

    private void chooseEnding()
    {
        if (choices.GetComponent<DMVGuyBusDialogue>().choice != 0 && choices.GetComponent<DMVGuyBusDialogue1>().choice != 0 && choice != 0)
        {
            int sum = choices.GetComponent<DMVGuyBusDialogue>().choice + choices.GetComponent<DMVGuyBusDialogue1>().choice + choice;
            if (sum < 4)
            {
                SceneManager.LoadScene(trunkIndex);
            }
            else
            {
                SceneManager.LoadScene(tortureIndex);
            }
        }
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
        tooLateAgain.enabled = false;
        nothingICanDo.enabled = false;
        player.transform.rotation = initialPlayer;
        transform.rotation = initialComputer;
        walkingScript.enabled = true;
        lookingScript.enabled = true;
        Cursor.lockState = CursorLockMode.Locked;
    }
}
