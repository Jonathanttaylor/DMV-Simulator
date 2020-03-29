using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PickANumber : MonoBehaviour
{
    public NowServingNumber nowserving;
    [SerializeField] TextMeshProUGUI number;

    Rect rectTicket;
    Rect rectNumber;
    Rect rectLetter;
    Texture texture;
    bool isInRange;
    bool displayNumber;
    private string ticketLetter;
    private int ticketNumber;
    readonly GUIStyle style = new GUIStyle();

    // Start is called before the first frame update
    void Start()
    {
        GameObject.Find("DMV_Number").GetComponent<Canvas>().enabled = false;
        nowserving = FindObjectOfType(typeof(NowServingNumber)) as NowServingNumber;

        //rectTicket = new Rect(Screen.width * 0.1f, Screen.height * 0.7f, Screen.width * 0.2f, Screen.width * 0.2f);
        //texture = Resources.Load<Texture2D>("Textures/number");

        //rectNumber = new Rect((Screen.width * 0.2f), (Screen.height * 0.9f), Screen.width * 0.3f, Screen.width * 0.3f);
        //rectLetter = new Rect((Screen.width * 0.18f), (Screen.height * 0.9f), Screen.width * 0.3f, Screen.width * 0.3f);

        //style.normal.textColor = Color.black;
        //style.fontSize = 20;

    }

    private void OnTriggerEnter(Collider other)
    {
        isInRange = true;
    }

    private void OnTriggerExit(Collider other)
    {
        isInRange = false;
    }

    // Update is called once per frame
    void Update()
    {
        TakeNumber();
        number.text = "A " + ticketNumber.ToString();
    }

    void TakeNumber()
    {
        if (isInRange && Input.GetKeyDown(KeyCode.E) && !displayNumber)
        {
            GameObject.Find("DMV_Number").GetComponent<Canvas>().enabled = true;
            ticketNumber = nowserving.GetNumber() + Random.Range(5, 15);
            Debug.Log(ticketNumber);
            print(ticketNumber);
            //ticketLetter = nowserving.GetLetter();
            //displayNumber = true;
        }
    }

    //private void OnGUI()
    //{
    //    if (displayNumber)
    //    {
    //        GUI.DrawTexture(rectTicket, texture);
    //        GUI.Label(rectLetter, ticketLetter.ToString(), style);
    //        GUI.Label(rectNumber, ticketNumber.ToString(), style);
    //    }
    //}
}
