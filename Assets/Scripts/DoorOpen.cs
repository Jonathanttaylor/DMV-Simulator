using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpen : MonoBehaviour
{
    private Animator animator;
    private bool isOpen = false;
    private bool isInRange = false;

    // Start is called before the first frame update
    void Start()
    {
        animator = transform.Find("Hinge").GetComponent<Animator>();
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
                animator.SetBool("open", false);
                isOpen = false;
            }
            else
            {
                animator.SetBool("open", true);
                isOpen = true;
            }
        }
    }


}
