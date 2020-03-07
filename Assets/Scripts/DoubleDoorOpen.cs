using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleDoorOpen : MonoBehaviour
{
    private Animator animatorLeft;
    private Animator animatorRight;
    private bool isOpen = false;
    private bool isInRange = false;

    // Start is called before the first frame update
    void Start()
    {
        animatorLeft = transform.Find("HingeLeft").GetComponent<Animator>();
        animatorRight = transform.Find("HingeRight").GetComponent<Animator>();
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
        interactDoor();
    }

    private void interactDoor()
    {
        if (isInRange && Input.GetKeyDown(KeyCode.E))
        {
            if (isOpen)
            {
                animatorLeft.SetBool("open", false);
                animatorRight.SetBool("open", false);
                isOpen = false;
            }
            else
            {
                animatorLeft.SetBool("open", true);
                animatorRight.SetBool("open", true);
                isOpen = true;
            }
        }
    }
}
