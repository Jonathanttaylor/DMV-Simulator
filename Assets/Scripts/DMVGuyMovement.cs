using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DMVGuyMovement : MonoBehaviour
{
    [SerializeField] Transform waypoint;
    [SerializeField] Transform waypoint2;
    [SerializeField] Transform waypoint3;
    [SerializeField] Transform waypoint4;
    private NavMeshAgent navMeshAgent;
    private BusMovement bus;
    private DMVGuyBusDialogue1 dialogue;
    private bool moved;
    private float timer = 4f;
    //private float rotationSpeed = 10f;
    private bool isSitting;

    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = this.GetComponent<NavMeshAgent>();
        bus = FindObjectOfType(typeof(BusMovement)) as BusMovement;
        dialogue = FindObjectOfType(typeof(DMVGuyBusDialogue1)) as DMVGuyBusDialogue1;
        animator = GetComponent<Animator>();
        moved = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Vector3 vectorToWaypoint = waypoint2.position;
            navMeshAgent.SetDestination(vectorToWaypoint);
        }

        if (bus.ReturnStop() == "stop1" && bus.GetCurrentSpeed() > -0.1 && !moved)
        {
            timer -= 1 * Time.deltaTime;

            if (timer < 0.1f)
            {
                Move(waypoint);
            }
        }

        if (bus.ReturnStop() == "stop2")
        {
            moved = false;
            timer = 4f;
        }

        if (bus.ReturnStop() == "stop5" && bus.GetCurrentSpeed() > -0.1 && !moved && dialogue.ReturnIsPressed())
        {
            Debug.Log("In DMVGuy Should be moving");

            animator.SetBool("sit", false);
            transform.parent = null;
            transform.position = waypoint3.position;
            transform.rotation = waypoint.rotation;
            GetComponent<NavMeshAgent>().enabled = true;

            timer -= 1 * Time.deltaTime;

            if (timer < 0.1f)
            {
                Move(waypoint2);
            }
        }

        if (IsInRangeOf(waypoint2))
        {
            GetComponent<NavMeshAgent>().enabled = false;
            transform.position = waypoint4.position;
        }

        if (IsInRangeOf(waypoint))
        {
            Sit();
        }
    }

    private void Move(Transform target)
    {
        Vector3 vectorToWaypoint = target.position;
        navMeshAgent.SetDestination(vectorToWaypoint);
        moved = true;
    }

    private bool IsInRangeOf(Transform target)
    {
        Debug.Log("CheckSit");
        float distance = Vector3.Distance(transform.position, target.position);
        return distance < 1.5f;
    }

    private void Sit()
    {
        Debug.Log("Sit");
        //Vector3 direction = (target.position - transform.position).normalized;
        //Quaternion lookRotation = Quaternion.LookRotation(direction);
        //transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
        if (!isSitting)
        {
            transform.rotation = waypoint.rotation;
            transform.position = waypoint.position;
            animator.SetBool("sit", true);
            isSitting = true;
            transform.parent = bus.transform;
            GetComponent<NavMeshAgent>().enabled = false;
        }
    }

}
