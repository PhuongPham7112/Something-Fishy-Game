using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    float turnSpeed;
    [SerializeField]
    float speed;
    [SerializeField]
    float upDownSpeed;
    [SerializeField]
    Transform waterPlane;

    private Rigidbody rb;
    private float timeCount = 0.0f;
    // Define the maximum and minimum rotation angles
    float maxRotationAngle = -60f;
    float minRotationAngle = -120f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        float yDifference = waterPlane.position.y - transform.position.y;

        // Moving Forward
        if (Input.GetKey(KeyCode.Space))
        {
            Vector3 forwardMovement = speed * Time.fixedDeltaTime * - transform.up;
            rb.MovePosition(rb.position + forwardMovement);
        }

        // Moving Up
        if (Input.GetKey(KeyCode.LeftShift) && yDifference > 0f)
        {
            Vector3 upMovement = Time.fixedDeltaTime * upDownSpeed * Vector3.up;
            rb.MovePosition(rb.position + upMovement);
        }

        // Moving Down
        if (Input.GetKey(KeyCode.LeftControl))
        {
            Vector3 downMovement = Time.fixedDeltaTime * upDownSpeed * Vector3.down;
            rb.MovePosition(rb.position + downMovement);
        }

        Quaternion newRotation = Quaternion.identity;
        // Rotating Left
        if (Input.GetKey(KeyCode.A))
        {
            newRotation = Quaternion.AngleAxis(-turnSpeed * Time.fixedDeltaTime, Vector3.forward);
            // Apply the angular velocity to the Rigidbody
            rb.MoveRotation(rb.rotation * newRotation);
        }
        // Rotating Right
        else if (Input.GetKey(KeyCode.D))
        {
            newRotation = Quaternion.AngleAxis(turnSpeed * Time.fixedDeltaTime, Vector3.forward);
            // Apply the angular velocity to the Rigidbody
            rb.MoveRotation(rb.rotation * newRotation);
        }
        // Turning down
        else if (Input.GetKey(KeyCode.S))
        {
            newRotation = Quaternion.AngleAxis(turnSpeed * Time.fixedDeltaTime, Vector3.right);
            Quaternion rotatedRotation = rb.rotation * newRotation;

            // Clamp the rotation angle between the minimum and maximum values
            float rotationX = Mathf.Clamp(rotatedRotation.eulerAngles.x, minRotationAngle, maxRotationAngle);
            rotatedRotation.eulerAngles = new Vector3(rotationX, rotatedRotation.eulerAngles.y, rotatedRotation.eulerAngles.z);

            // Apply the angular velocity to the Rigidbody
            rb.MoveRotation(Quaternion.Slerp(rb.rotation, rotatedRotation, 5.0f * Time.deltaTime));
        }
        // Turning up
        else if (Input.GetKey(KeyCode.W) && yDifference > 0f)
        {
            newRotation = Quaternion.AngleAxis(-turnSpeed * Time.fixedDeltaTime, Vector3.right);
            Quaternion rotatedRotation = rb.rotation * newRotation;

            // Clamp the rotation angle between the minimum and maximum values
            float rotationX = Mathf.Clamp(rotatedRotation.eulerAngles.x, minRotationAngle, maxRotationAngle);
            rotatedRotation.eulerAngles = new Vector3(rotationX, rotatedRotation.eulerAngles.y, rotatedRotation.eulerAngles.z);

            // Apply the angular velocity to the Rigidbody
            rb.MoveRotation(Quaternion.Slerp(rb.rotation, rotatedRotation, 5.0f * Time.deltaTime));
        }
        else
        {
            Quaternion defaultRotation = Quaternion.Euler(-90f, transform.localEulerAngles.y, transform.localEulerAngles.z);
            rb.MoveRotation(Quaternion.Slerp(rb.rotation, defaultRotation, 20.0f * Time.deltaTime));
            if (timeCount > 1.5f)
            {
                // Ensure that the Rigidbody is completely stopped
                rb.velocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
                timeCount = 0.0f;
            }
            
        }
        timeCount += Time.deltaTime;
    }
}
