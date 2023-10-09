using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class WaterPlane : MonoBehaviour
{
    public PlayerMovement playerMovement;
    public float maxYPosition = -10.0f;
    public float targetYPosition = -27.5f; // The target Y position.
    public float duration = 300f; // The duration in seconds (10 minutes).
    public float increaseDuration = 60f;

    private Vector3 initialPosition; // The initial position of the GameObject.
    private float startTime; // The start time of the lowering process.
    private bool isDraining = true;

    private void Start()
    {
        initialPosition = transform.position;
        maxYPosition = initialPosition.y + 10f;
        targetYPosition = initialPosition.y - 30f;
        startTime = Time.time;
        StartCoroutine(LowerYPosition());
    }

    private IEnumerator LowerYPosition()
    {
        SetDrainStatus(true);
        while (transform.position.y > targetYPosition && isDraining)
        {
            float elapsedTime = Time.time - startTime;
            float newYPosition = Mathf.Lerp(initialPosition.y, targetYPosition, elapsedTime / duration);

            // Apply the new position to the GameObject.
            transform.position = new Vector3(transform.position.x, newYPosition, transform.position.z);

            yield return null;
        }
        Debug.Log("Finish draining" + transform.position.y + " " + targetYPosition);
        if (!GameState.isEscape && !GameState.isValveTurned)
        {
            Debug.Log("Escape game, water drained");
            SceneStateManager.Instance.PlayEndScene();
        }
    }

    private IEnumerator IncreaseYPosition()
    {
        while (transform.position.y < maxYPosition)
        {
            float elapsedTime = Time.time - startTime;
            float newYPosition = Mathf.Lerp(initialPosition.y, maxYPosition, elapsedTime / increaseDuration);

            // Apply the new position to the GameObject.
            transform.position = new Vector3(transform.position.x, newYPosition, transform.position.z);

            yield return null;
        }
        GameState.isValveTurned = false; // done refilling from valve turn
        Debug.Log("Done refilling");
        DrainWaterLevel(); // Draining again
    }

    public void RestoreWaterLevel()
    {
        StopCoroutine(LowerYPosition());
        SetDrainStatus(false);
        initialPosition = transform.position;
        startTime = Time.time;
        
        StartCoroutine(IncreaseYPosition());
    }

    void DrainWaterLevel()
    {
        StopCoroutine(IncreaseYPosition());
        SetDrainStatus(true);
        initialPosition = transform.position;
        startTime = Time.time;

        StartCoroutine(LowerYPosition());
    }

    private void SetDrainStatus(bool status)
    {
        isDraining = status;
        playerMovement.isWaterDraining = status;
    }
}
