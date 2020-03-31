using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DMVGuyMovement : MonoBehaviour
{
    [SerializeField] Transform waypoint;
    private NavMeshAgent navMeshAgent;
    private float rotationSpeed = 10f;

    private bool moving;

    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = this.GetComponent<NavMeshAgent>();

        moving = true;
    }

    // Update is called once per frame
    void Update()
    {
        Move();

        if (navMeshAgent.remainingDistance <= 1.0f)
        {
            Debug.Log("Here");
            RotateTowards(waypoint);
        }
    }

    private void Move()
    {
        if (moving)
        {
            Vector3 vectorToWaypoint = waypoint.position;
            navMeshAgent.SetDestination(vectorToWaypoint);
            moving = false;
        }
    }

    private void RotateTowards(Transform target)
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
    }
}
