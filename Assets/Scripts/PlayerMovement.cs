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
    [SerializeField]
    ParticleSystem particle;

    public bool isWaterDraining = true;
    private Rigidbody rb;
    private float timeCount = 0.0f;
    // Define the maximum and minimum rotation angles
    float maxRotationAngle = -60f;
    float minRotationAngle = -120f;
    float maxVelocity = 10f;
    float startDescendingTime;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (particle.isPlaying) particle.Stop();
    }

    void FixedUpdate()
    {
        if (GameState.isPlaying)
        {
            
            float yDifference = waterPlane.position.y - transform.position.y;
            bool reachMaxWaterLevel = yDifference > 0f;
            bool isMoving = false;

            // Check if the current Y position is higher than the maximum allowed.
            if (yDifference < 0f)
            {
                // Calculate the new Y position after descending.
                float newYPosition = waterPlane.position.y;

                if (isWaterDraining)
                {
                    // Set the new Y position while preserving the object's X and Z coordinates.
                    rb.MovePosition(new Vector3(transform.position.x, newYPosition, transform.position.z));
                }
            }

            float velocityMag = rb.velocity.magnitude;
            Quaternion newRotation = Quaternion.identity;
            // Moving Forward
            if (Input.GetKey(KeyCode.Space) && !Input.GetKey(KeyCode.LeftShift))
            {
                isMoving = true;
                Vector3 forwardMovement = speed  * -transform.up;
                rb.AddForce(forwardMovement, ForceMode.VelocityChange);
                if (velocityMag > maxVelocity)
                {
                    rb.velocity = rb.velocity.normalized * maxVelocity;
                }
                if (particle.isStopped) particle.Play();
            }
            // Move twice as fast
            else if (Input.GetKey(KeyCode.Space) && Input.GetKey(KeyCode.LeftShift))
            {
                isMoving = true;
                Vector3 forwardMovement = 4f * speed  * -transform.up;
                rb.AddForce(forwardMovement, ForceMode.VelocityChange);
                if (velocityMag > maxVelocity)
                {
                    rb.velocity = rb.velocity.normalized * maxVelocity;
                }
                if (particle.isStopped) particle.Play();
            } 

            // Rotating Left
            if (Input.GetKey(KeyCode.A))
            {
                isMoving = true;
                newRotation = Quaternion.AngleAxis(-turnSpeed * Time.fixedDeltaTime, Vector3.forward);
                // Apply the angular velocity to the Rigidbody
                rb.MoveRotation(rb.rotation * newRotation);
            }
            // Rotating Right
            else if (Input.GetKey(KeyCode.D))
            {
                isMoving = true;
                newRotation = Quaternion.AngleAxis(turnSpeed * Time.fixedDeltaTime, Vector3.forward);
                // Apply the angular velocity to the Rigidbody
                rb.MoveRotation(rb.rotation * newRotation);
            }
            // Turning down
            else if (Input.GetKey(KeyCode.S))
            {
                isMoving = true;
                newRotation = Quaternion.AngleAxis(turnSpeed * Time.fixedDeltaTime, Vector3.right);
                Quaternion rotatedRotation = rb.rotation * newRotation;

                // Clamp the rotation angle between the minimum and maximum values
                float rotationX = Mathf.Clamp(rotatedRotation.eulerAngles.x, minRotationAngle, maxRotationAngle);
                rotatedRotation.eulerAngles = new Vector3(rotationX, rotatedRotation.eulerAngles.y, rotatedRotation.eulerAngles.z);

                // Apply the angular velocity to the Rigidbody
                rb.MoveRotation(Quaternion.Slerp(rb.rotation, rotatedRotation, 5.0f * Time.deltaTime));

                // Move down
                Vector3 downMovement = Time.fixedDeltaTime * upDownSpeed * Vector3.down;
                rb.AddForce(downMovement, ForceMode.VelocityChange);
                if (velocityMag > maxVelocity)
                {
                    rb.velocity = rb.velocity.normalized * maxVelocity;
                }
                if (particle.isStopped) particle.Play();
            }
            // Turning up
            else if (Input.GetKey(KeyCode.W) && reachMaxWaterLevel)
            {
                isMoving = true;
                newRotation = Quaternion.AngleAxis(-turnSpeed * Time.fixedDeltaTime, Vector3.right);
                Quaternion rotatedRotation = rb.rotation * newRotation;

                // Clamp the rotation angle between the minimum and maximum values
                float rotationX = Mathf.Clamp(rotatedRotation.eulerAngles.x, minRotationAngle, maxRotationAngle);
                rotatedRotation.eulerAngles = new Vector3(rotationX, rotatedRotation.eulerAngles.y, rotatedRotation.eulerAngles.z);

                // Apply the angular velocity to the Rigidbody
                rb.MoveRotation(Quaternion.Slerp(rb.rotation, rotatedRotation, 5.0f * Time.deltaTime));

                // Move up
                Vector3 upMovement = Time.fixedDeltaTime * upDownSpeed * Vector3.up;
                rb.AddForce(upMovement, ForceMode.VelocityChange);
                if (velocityMag > maxVelocity)
                {
                    rb.velocity = rb.velocity.normalized * maxVelocity;
                }
                if (particle.isStopped) particle.Play();
            }
            // if not rotating
            else
            {
                // Reset tilting
                Quaternion defaultRotation = Quaternion.Euler(-90f, transform.localEulerAngles.y, transform.localEulerAngles.z);
                rb.MoveRotation(Quaternion.Slerp(rb.rotation, defaultRotation, 20.0f * Time.deltaTime));
            }
            
            if (!isMoving)
            {
                if (particle.isPlaying) particle.Stop();
                // Ensure that the Rigidbody is completely stopped
                rb.velocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
                timeCount = 0.0f;
            }
            timeCount += Time.deltaTime;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Move(Vector3 direction, float velocityMagnitude)
    {
        rb.AddForce(direction, ForceMode.VelocityChange);
        if (velocityMagnitude > maxVelocity)
        {
            rb.velocity = rb.velocity.normalized * maxVelocity;
        }
        if (particle.isStopped) particle.Play();
    }
}
