using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BusMovement : MonoBehaviour
{
    // Setting Serialized Fields
    [SerializeField] Transform busRoute;
    [SerializeField] Transform startRoute;
    [SerializeField] WheelCollider wheelFL;
    [SerializeField] WheelCollider wheelFR;
    [SerializeField] WheelCollider wheelRL;
    [SerializeField] WheelCollider wheelRR;
    [SerializeField] float currentSpeed;
    [SerializeField] float maxTurnAngle = 45f;
    [SerializeField] float maxSpeed = 3500;
    [SerializeField] float lowMotorTorque;
    [SerializeField] float highMotorTorque;
    [SerializeField] float brakeTorque = 1500;
    [SerializeField] float stopTime;
    [SerializeField] float startStopTime;
    [SerializeField] float waitTime;

    // Setting private members
    private List<Transform> waypoints;
    private int currentWaypoint;
    private bool isBraking;
    private bool stopped;
    private bool startStopped;
    private float motorTorque;

    private bool onBus;
    private List<Transform> startWaypoints;
    private int startCurrentWaypoint = 0;

    private BusDoors doors;
    private BusDriverDialogue driver;

    [SerializeField] NavMeshSurface surface;

    private string stop;
    private bool meshBuilt;
    private float delay;

    // Start is called before the first frame update
    void Start()
    {
        motorTorque = lowMotorTorque;

        doors = FindObjectOfType(typeof(BusDoors)) as BusDoors;
        driver = FindObjectOfType(typeof(BusDriverDialogue)) as BusDriverDialogue;

        onBus = false;
        isBraking = true;
        waitTime = 150f;

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

        startWaypoints = new List<Transform>();
        Transform[] startrouteWaypoints = startRoute.GetComponentsInChildren<Transform>();

        for (int i = 0; i < busrouteWaypoints.Length; i++)
        {
            if (startrouteWaypoints[i] != startRoute.transform)
            {
                startWaypoints.Add(startrouteWaypoints[i]);
            }
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // Setting current speed
        currentSpeed = 2 * Mathf.PI * wheelFL.radius * wheelFL.rpm * 60 / 100;

        Drive();
        Turn();
        ChangeWaypoint();
        Braking();
        StopTimer();
        StartStopTimer();
        WaitTimer();
        AtStop();
    }

    void StartStopTimer()
    {
        if (startStopped && !onBus)
        {
            startStopTime -= 1 * Time.deltaTime;

            if (startStopTime < 1)
            {
                isBraking = false;
                startStopped = false;
            }
        }
    }

    void StopTimer()
    {
        if (stopped)
        {
            stopTime -= 1 * Time.deltaTime;

            if (stopTime < 1)
            {
                isBraking = false;
                stopped = false;
            }
        }
    }

    void WaitTimer()
    {
        if (!onBus)
        {
            waitTime -= 1 * Time.deltaTime;

            if (waitTime < 1)
            {
                stopped = true;
                stopTime = 5f;
                transform.position = new Vector3(1.71f, 1.43f, -39.42f);
                transform.rotation = Quaternion.Euler(0f, 90f, 0f);
                startCurrentWaypoint = 0;
                waitTime = 210f;
            }
        }
    }

    void AtStop()
    {
        if (stop == "stop1" && currentSpeed > -0.1 && !meshBuilt)
        {
            surface.BuildNavMesh();
            doors.ToggleDoors();
            meshBuilt = true;
        }
        else if (stop == "stop1" && !stopped)
        {
            doors.ToggleDoors();
            stop = null;
        }
        else if (stop == "stop5" && currentSpeed > -0.1 && !meshBuilt)
        {
            delay -= 1 * Time.deltaTime;

            if (delay < 1)
            {
                surface.BuildNavMesh();
                doors.SetStayOpen();
                meshBuilt = true;
            }
        }
    }

    // Powers and unpowers the busses wheels
    void Drive()
    {
        if (currentSpeed > -maxSpeed && !isBraking)
        {
            wheelFL.motorTorque = -motorTorque * Time.deltaTime;
            wheelFR.motorTorque = -motorTorque * Time.deltaTime;
        }
        else
        {
            wheelFL.motorTorque = 0 * Time.deltaTime;
            wheelFR.motorTorque = 0 * Time.deltaTime;
        }
    }

    // Turns bus towards next waypoint
    void Turn()
    {
        if (!onBus)
        {
            Vector3 vectorToWaypoint = -transform.InverseTransformPoint(startWaypoints[startCurrentWaypoint].position);
            float newTurnAngle = (vectorToWaypoint.x / vectorToWaypoint.magnitude) * maxTurnAngle;
            wheelFL.steerAngle = newTurnAngle;
            wheelFR.steerAngle = newTurnAngle;
        }
        else if (onBus)
        {
            Vector3 vectorToWaypoint = -transform.InverseTransformPoint(waypoints[currentWaypoint].position);
            float newTurnAngle = (vectorToWaypoint.x / vectorToWaypoint.magnitude) * maxTurnAngle;
            wheelFL.steerAngle = newTurnAngle;
            wheelFR.steerAngle = newTurnAngle;
        }
    }

    // Switches to next waypoint when bus is near
    void ChangeWaypoint()
    {
        if (Vector3.Distance(transform.position, startWaypoints[startCurrentWaypoint].position) < 1f && !onBus)
        {
            if (startCurrentWaypoint == 0)
            {
                driver.SetHasInteracted();
                doors.SetRejectedFalse();
                motorTorque = lowMotorTorque;
                isBraking = true;
                stopTime = 5f;
                stopped = true;
            }
            else if (startCurrentWaypoint == 3)
            {
                isBraking = true;
                startStopTime = 20f;
                startStopped = true;
            }
            else if (startCurrentWaypoint == 4)
            {
                motorTorque = highMotorTorque;
            }
            else if (startCurrentWaypoint == 13)
            {
                isBraking = true;
            }

            startCurrentWaypoint++;
        }

        if (Vector3.Distance(transform.position, waypoints[currentWaypoint].position) < 1f && onBus)
        {
            if (currentWaypoint == 0)
            {
                motorTorque = highMotorTorque;
            }
            else if (currentWaypoint == 16)
            {
                doors.SetRejectedFalse();
                isBraking = true;
                stopTime = 20f;
                stopped = true;
                meshBuilt = false;
                delay = 2f;
                stop = "stop1";
            }
            else if (currentWaypoint == 35)
            {
                isBraking = true;
                stopTime = 20f;
                stopped = true;
                stop = "stop2";
            }
            else if (currentWaypoint == 41)
            {
                isBraking = true;
                stopTime = 10f;
                stopped = true;
                motorTorque = lowMotorTorque;
            }
            else if (currentWaypoint == 45)
            {
                isBraking = true;
                stopTime = 6f;
                stopped = true;
            }
            else if (currentWaypoint == 49)
            {
                motorTorque = highMotorTorque;
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
                meshBuilt = false;
                delay = 2f;
                stop = "stop5";
            }

            currentWaypoint++;
        }
    }

    // Stops the bus
    void Braking()
    {
        if (isBraking)
        {
            wheelRL.brakeTorque = brakeTorque * Time.deltaTime;
            wheelRR.brakeTorque = brakeTorque * Time.deltaTime;
        }
        else
        {
            wheelRL.brakeTorque = 0 * Time.deltaTime;
            wheelRR.brakeTorque = 0 * Time.deltaTime;
        }
    }

    // Sets startDriving flag to true when called
    public void StartDriving()
    {
        isBraking = false;
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

    public void SetOnBus()
    {
        onBus = true;
    }

    public int ReturnCurrentWaypoint()
    {
        return currentWaypoint;
    }

    public bool ReturnOnBus()
    {
        return onBus;
    }

    public string ReturnStop()
    {
        return stop;
    }
}
