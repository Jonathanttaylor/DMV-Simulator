using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DMVGuyMovement : MonoBehaviour
{
    [SerializeField] Transform waypoint;
    private NavMeshAgent navMeshAgent;
    private BusMovement bus;
    private bool moved;
    private float timer = 4f;
    private bool sit;
    //private float rotationSpeed = 10f;
    private bool isSitting;

    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = this.GetComponent<NavMeshAgent>();
        bus = FindObjectOfType(typeof(BusMovement)) as BusMovement;
        animator = GetComponent<Animator>();
        moved = false;
        sit = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (bus.ReturnStop() == "stop1" && bus.GetCurrentSpeed() > -0.1 && !moved)
        {
            timer -= 1 * Time.deltaTime;

            if (timer < 0.1f)
            {
                Move();
            }
        }

        if (IsInRangeOfChair())
        {
            Sit();
        }
    }

    private void Move()
    {
        Vector3 vectorToWaypoint = waypoint.position;
        navMeshAgent.SetDestination(vectorToWaypoint);
        moved = true;
    }

    private bool IsInRangeOfChair()
    {
        float distance = Vector3.Distance(transform.position, waypoint.position);
        return distance < 0.5f;
    }

    private void Sit()
    {
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
