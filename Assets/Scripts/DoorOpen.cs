using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpen : MonoBehaviour
{
    private Animator animatorLeft;
    private Animator animatorRight;
    private bool isOpen = false;
    private bool isInRange = false;
    private bool leftHinge = false;
    private bool rightHinge = false;

    // Start is called before the first frame update
    void Start()
    {
        if (transform.Find("HingeLeft"))
        {
            animatorLeft = transform.Find("HingeLeft").GetComponent<Animator>();
            leftHinge = true;
        }

        if (transform.Find("HingeRight"))
        {
            animatorRight = transform.Find("HingeRight").GetComponent<Animator>();
            rightHinge = true;
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
                if (leftHinge)
                {
                    animatorLeft.SetBool("open", false);
                }

                if (rightHinge)
                {
                    animatorRight.SetBool("open", false);
                }

                isOpen = false;
            }
            else
            {
                if (leftHinge)
                {
                    animatorLeft.SetBool("open", true);
                }

                if (rightHinge)
                {
                    animatorRight.SetBool("open", true);
                }
                
                isOpen = true;
            }
        }
    }


}
