using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorCloseEnding : MonoBehaviour
{
    private Animator animatorLeft;
    private Animator animatorRight;
    private bool isOpen = false;
    private bool isInRange = false;
    private bool leftHinge = false;
    private bool rightHinge = false;
    public bool closeDoor = false;

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

         //animatorLeft.SetBool("open", true);
    }

    // Update is called once per frame
    void Update()
    {
        InteractDoor();
    }

    private void InteractDoor()
    {
        if (closeDoor)
        {
            animatorLeft.SetBool("open", true);
            /*
            if (leftHinge)
            {
                animatorLeft.SetBool("open", false);
            }

            if (rightHinge)
            {
                animatorRight.SetBool("open", false);
            }
            */
        }
    }
}
