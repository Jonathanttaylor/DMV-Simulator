using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PickANumber : MonoBehaviour
{
    public NowServingNumber nowserving;
    [SerializeField] TextMeshProUGUI number;
    bool isInRange;
    public int ticketNumber = 1;
    public bool displayTicket = false;
    private GameObject DMVNumber;

    // Start is called before the first frame update
    void Start()
    {
        DMVNumber = GameObject.Find("DMV_Number");
        DMVNumber.GetComponent<Canvas>().enabled = false;
        nowserving = FindObjectOfType(typeof(NowServingNumber)) as NowServingNumber;
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
        TakeNumber();
        number.text = "A " + ticketNumber.ToString();
    }

    void TakeNumber()
    {
        if (isInRange && Input.GetKeyDown(KeyCode.E) && ! displayTicket)
        {
            DMVNumber.GetComponent<Canvas>().enabled = true;

            ticketNumber = nowserving.GetNumber() + Random.Range(5, 15);
            displayTicket = true;

 
            
        }
    }

    public bool hasTakenNumber()
    {
        return displayTicket;
    }

    public void setDisplayTicket()
    {
        displayTicket = false;
    }

}
