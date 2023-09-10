using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterPlane : MonoBehaviour
{
    public float maxYPosition = -4.0f;
    public float targetYPosition = -27.5f; // The target Y position.
    public float duration = 10f; // The duration in seconds (10 minutes).

    private Vector3 initialPosition; // The initial position of the GameObject.
    private float startTime; // The start time of the lowering process.

    private void Start()
    {
        initialPosition = transform.position;
        startTime = Time.time;
        StartCoroutine(LowerYPosition());
    }

    private IEnumerator LowerYPosition()
    {
        while (transform.position.y > targetYPosition)
        {
            float elapsedTime = Time.time - startTime;
            float newYPosition = Mathf.Lerp(initialPosition.y, targetYPosition, elapsedTime / duration);

            // Apply the new position to the GameObject.
            transform.position = new Vector3(transform.position.x, newYPosition, transform.position.z);

            yield return null;
        }
    }

    private IEnumerator IncreaseYPosition()
    {
        while (transform.position.y < maxYPosition)
        {
            float elapsedTime = Time.time - startTime;
            float newYPosition = Mathf.Lerp(initialPosition.y, maxYPosition, elapsedTime / duration);

            // Apply the new position to the GameObject.
            transform.position = new Vector3(transform.position.x, newYPosition, transform.position.z);

            yield return null;
        }
    }

    public void RestoreWaterLevel()
    {
        StopCoroutine(LowerYPosition());
        initialPosition = transform.position;
        startTime = Time.time;
        StartCoroutine(IncreaseYPosition());
    }
}
