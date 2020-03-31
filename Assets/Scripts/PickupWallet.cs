using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupWallet : MonoBehaviour
{
    public bool isPickedup = false;
    private bool isInRange = false;
    [SerializeField] GameObject walletpart1;
    [SerializeField] GameObject walletpart2;
    [SerializeField] GameObject walletpart3;
    [SerializeField] GameObject walletpart4;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        WalletPickup();
    }

    private void WalletPickup()
    {
        if (isInRange && !isPickedup && Input.GetKey(KeyCode.E))
        {
            isPickedup = true;
            walletpart1.GetComponent<Renderer>().enabled = false;
            walletpart2.GetComponent<Renderer>().enabled = false;
            walletpart3.GetComponent<Renderer>().enabled = false;
            walletpart4.GetComponent<Renderer>().enabled = false;
        }
    }

    private void OnTriggerEnter(Collider collide)
    {
        if (collide.tag == "Wallet")
        { 
            isInRange = true;
        }
    }

    private void OnTriggerExit(Collider collide)
    {
        if (collide.tag == "Wallet")
        {
            isInRange = false;
        }
    }
}
