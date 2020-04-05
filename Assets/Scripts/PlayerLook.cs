using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    [SerializeField] float sensitivity = 100f;
    [SerializeField] float maxAngle = 90f;
    [SerializeField] Transform player;

    private float xRot = 0f;
    private float yRot = 0f;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; //Locks cursor to center view
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        LookAround();
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

    public void SittingLookAround()
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
