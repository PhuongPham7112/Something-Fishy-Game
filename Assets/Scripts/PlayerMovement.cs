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

    private Rigidbody rb;
    private CapsuleCollider capsuleCollider;
    private GameObject viewObject;
    private float timeCount = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        capsuleCollider = GetComponent<CapsuleCollider>();
    }

    // W = up, A = left, D = right, S = down
    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        // Moving Forward
        if (Input.GetKey(KeyCode.LeftShift))
        {
            Vector3 forwardMovement = - transform.up * speed * Time.fixedDeltaTime;
            rb.MovePosition(rb.position + forwardMovement);
        }

        // Moving Up
        if (Input.GetKey(KeyCode.Space))
        {
            Vector3 upMovement = Vector3.up * upDownSpeed * Time.fixedDeltaTime;
            rb.MovePosition(rb.position + upMovement);
        }

        // Moving Down
        if (Input.GetKey(KeyCode.LeftControl))
        {
            Vector3 downMovement = Vector3.down * upDownSpeed * Time.fixedDeltaTime;
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
            // Apply the angular velocity to the Rigidbody
            rb.MoveRotation(rb.rotation * newRotation);
        }
        // Turning up
        else if (Input.GetKey(KeyCode.W))
        {
            newRotation = Quaternion.AngleAxis(-turnSpeed * Time.fixedDeltaTime, Vector3.right);
            // Apply the angular velocity to the Rigidbody
            rb.MoveRotation(rb.rotation * newRotation);
        }
        else
        {
            Quaternion defaultRotation = Quaternion.Euler(-90f, transform.localEulerAngles.y, transform.localEulerAngles.z);
            rb.MoveRotation(Quaternion.Slerp(rb.rotation, defaultRotation, 20.0f * Time.deltaTime));
            if (timeCount > 3.0f)
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
