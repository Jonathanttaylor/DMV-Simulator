using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BusDoors : MonoBehaviour
{
    private Animator animator;
    private bool isOpen = false;
    private bool toggleDoors = false;
    public bool rejected = false;
    public bool stayOpen = false;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider collide)
    {
        if (collide.CompareTag("Player"))
        {
            toggleDoors = true;
        }
    }

    private void OnTriggerExit(Collider collide)
    {
        if (collide.CompareTag("Player"))
        {
            toggleDoors = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        InteractDoor();
    }

    private void InteractDoor()
    {
        if (rejected)
        {
            animator.SetBool("open", false);

            toggleDoors = false;
            isOpen = false;
        }
        else if (stayOpen)
        {
            animator.SetBool("open", true);
        }
        else if (toggleDoors)
        {
            if (isOpen)
            {
                animator.SetBool("open", false);

                toggleDoors = false;
                isOpen = false;
            }
            else
            {
                animator.SetBool("open", true);

                toggleDoors = false;
                isOpen = true;
            }
        }
    }

    public void SetRejectedFalse()
    {
        rejected = false;
    }

    public void SetRejectedTrue()
    {
        rejected = true;
    }

    public void ToggleDoors()
    {
        toggleDoors = true;
    }

    public void SetStayOpen()
    {
        stayOpen = true;
    }
}
