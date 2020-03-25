using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BusMovement : MonoBehaviour
{
    // Setting Serialized Fields
    [SerializeField] Transform busroute;
    [SerializeField] WheelCollider wheelFL;
    [SerializeField] WheelCollider wheelFR;
    [SerializeField] WheelCollider wheelRL;
    [SerializeField] WheelCollider wheelRR;
    [SerializeField] float currentSpeed;
    [SerializeField] float maxTurnAngle = 45f;
    [SerializeField] float maxSpeed = 500;
    [SerializeField] float motorTorque = 300;
    [SerializeField] float brakeTorque = 1500;

    // Setting private members
    private List<Transform> waypoints;
    private int currentWaypoint = 0;
    private bool isBraking;
    private bool startDriving;

    // Start is called before the first frame update
    void Start()
    {
        // Creating a list containing all busroute waypoints
        Transform[] busrouteWaypoints = busroute.GetComponentsInChildren<Transform>();
        waypoints = new List<Transform>();

        for (int i = 0; i < busrouteWaypoints.Length; i++)
        {
            if (busrouteWaypoints[i] != busroute.transform)
            {
                waypoints.Add(busrouteWaypoints[i]);
            }
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // Setting current speed
        currentSpeed = 2 * Mathf.PI * wheelFL.radius * wheelFL.rpm * 60 / 100;

        if (startDriving)
        {
            Drive();
        }

        Turn();

        ChangeWaypoint();

        Braking();
    }

    // Powers and unpowers the busses wheels
    void Drive()
    {
        if (currentSpeed > -maxSpeed && !isBraking)
        {
            wheelFL.motorTorque = -motorTorque;
            wheelFR.motorTorque = -motorTorque;
        }
        else
        {
            wheelFL.motorTorque = 0;
            wheelFR.motorTorque = 0;
        }
    }

    // Turns bus towards next waypoint
    void Turn()
    {
        Vector3 vectorToWaypoint = -transform.InverseTransformPoint(waypoints[currentWaypoint].position);
        float newTurnAngle = (vectorToWaypoint.x / vectorToWaypoint.magnitude) * maxTurnAngle;
        wheelFL.steerAngle = newTurnAngle;
        wheelFR.steerAngle = newTurnAngle;
    }

    // Switches to next waypoint when bus is near
    void ChangeWaypoint()
    {
        if (Vector3.Distance(transform.position, waypoints[currentWaypoint].position) < 1f)
        {
            if (currentWaypoint == waypoints.Count - 1)
            {
                isBraking = true;
            }
            else
            {
                currentWaypoint++;
            }
        }
    }

    // Stops the bus
    void Braking()
    {
        if (isBraking)
        {
            wheelRL.brakeTorque = brakeTorque;
            wheelRR.brakeTorque = brakeTorque;
        }
        else
        {
            wheelRL.brakeTorque = 0;
            wheelRR.brakeTorque = 0;
        }
    }

    // Sets startDriving flag to true when called
    public void StartDriving()
    {
        startDriving = true;
    }

    // accessor for isBraking flag
    public bool GetIsBraking()
    {
        return isBraking;
    }

    // accessor for currentspeed
    public float GetCurrentSpeed()
    {
        return currentSpeed;
    }
}
