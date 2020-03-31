using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BusMovement : MonoBehaviour
{
    // Setting Serialized Fields
    [SerializeField] Transform busRoute;
    [SerializeField] WheelCollider wheelFL;
    [SerializeField] WheelCollider wheelFR;
    [SerializeField] WheelCollider wheelRL;
    [SerializeField] WheelCollider wheelRR;
    [SerializeField] float currentSpeed;
    [SerializeField] float maxTurnAngle = 45f;
    [SerializeField] float maxSpeed = 500;
    [SerializeField] float motorTorque = 300;
    [SerializeField] float brakeTorque = 1500;
    [SerializeField] float stopTime;

    // Setting private members
    private List<Transform> waypoints;
    private int currentWaypoint;
    private bool isBraking;
    private bool startDriving;
    private bool stopped;

    // Start is called before the first frame update
    void Start()
    {
        //isBraking = true;
        stopTime = 20f;

        // Creating a list containing all busroute waypoints
        waypoints = new List<Transform>();
        Transform[] busrouteWaypoints = busRoute.GetComponentsInChildren<Transform>();

        for (int i = 0; i < busrouteWaypoints.Length; i++)
        {
            if (busrouteWaypoints[i] != busRoute.transform)
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

        if (Input.GetKeyDown(KeyCode.Q))
        {
            isBraking = false;
        }

        StopTimer();
    }

    void StopTimer()
    {
        if (stopped)
        {
            stopTime -= 1 * Time.deltaTime; ;

            if (stopTime < 1)
            {
                isBraking = false;
                stopped = false;
            }
        }
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
            if (currentWaypoint == 16)
            {
                isBraking = true;
                stopTime = 20f;
                stopped = true;
            }
            else if (currentWaypoint == 35)
            {
                isBraking = true;
                stopTime = 20f;
                stopped = true;
            }
            else if (currentWaypoint == 41)
            {
                isBraking = true;
                stopTime = 10f;
                stopped = true;
                motorTorque = 100;
            }
            else if (currentWaypoint == 45)
            {
                isBraking = true;
                stopTime = 6f;
                stopped = true;
            }
            else if (currentWaypoint == 49)
            {
                motorTorque = 300;
            }
            else if (currentWaypoint == 57)
            {
                isBraking = true;
                stopTime = 20f;
                stopped = true;
            }
            else if (currentWaypoint == 65)
            {
                isBraking = true;
                stopTime = 20f;
                stopped = true;
            }
            else if (currentWaypoint == 73)
            {
                isBraking = true;
            }


            currentWaypoint++;
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
