using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupWallet : MonoBehaviour
{
    [SerializeField] bool isPickedup = false;
    private bool isInRange = false;
    [SerializeField] GameObject wallet;
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
            Destroy(wallet);
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
