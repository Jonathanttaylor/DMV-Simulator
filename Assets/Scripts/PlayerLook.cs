using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    public Sitting sit;

    [SerializeField] float sensitivity = 100f;
    [SerializeField] float maxAngle = 90f;
    [SerializeField] Transform player;

    private float xRot = 0f;
    private float yRot = 0f;

    // Start is called before the first frame update
    void Start()
    {
        sit = FindObjectOfType(typeof(Sitting)) as Sitting;
        Cursor.lockState = CursorLockMode.Locked; //Locks cursor to center view
    }

    // Update is called once per frame
    void Update()
    {
        if (!sit.isSitting())
        {
            LookAround();
        }

        if (sit.isSitting())
        {
            SittingLookAround();
        }

    }

    private void LookAround()
    {
        float mouseX = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;

        player.Rotate(Vector3.up * mouseX); //Rotates about the y-axis to look left and right

        xRot -= mouseY; //updates the rotation on the x-axis based on the mouse input on the y-axis
        xRot = Mathf.Clamp(xRot, -maxAngle, maxAngle); //restricts the rotation to our maxAngle;

        transform.localRotation = Quaternion.Euler(xRot, 0f, 0f);
        player.Rotate(Vector3.up * mouseX);
    }

    private void SittingLookAround()
    {
        float mouseX = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;

        xRot -= mouseY;
        xRot = Mathf.Clamp(xRot, -maxAngle, maxAngle);

        yRot += mouseX;
        yRot = Mathf.Clamp(yRot, -maxAngle, maxAngle);

        transform.localRotation = Quaternion.Euler(xRot, yRot, 0f);
    }
}
