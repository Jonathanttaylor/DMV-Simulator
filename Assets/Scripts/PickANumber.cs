using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PickANumber : MonoBehaviour
{
    public NowServingNumber nowserving;
    [SerializeField] TextMeshProUGUI number;
    bool isInRange;
    private int ticketNumber;
    bool displayTicket = false;

    // Start is called before the first frame update
    void Start()
    {
        GameObject.Find("DMV_Number").GetComponent<Canvas>().enabled = false;
        nowserving = FindObjectOfType(typeof(NowServingNumber)) as NowServingNumber;
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
        if (isInRange && Input.GetKeyDown(KeyCode.E) && ! displayTicket)
        {
            GameObject.Find("DMV_Number").GetComponent<Canvas>().enabled = true;

            ticketNumber = nowserving.GetNumber() + Random.Range(5, 15);
            displayTicket = true;

 
            
        }
    }
}
