using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnBusTrigger : MonoBehaviour
{
    private BusMovement bus;
    private BusDoors doors;
    private bool first;

    // Start is called before the first frame update
    void Start()
    {
        bus = FindObjectOfType(typeof(BusMovement)) as BusMovement;
        doors = FindObjectOfType(typeof(BusDoors)) as BusDoors;
        first = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !first)
        {
            bus.SetOnBus();
            doors.SetRejectedTrue();
            first = true;
        }
    }
}
