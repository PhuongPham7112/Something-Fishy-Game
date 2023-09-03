using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoverText : MonoBehaviour
{
    private GameObject player;

    private void Start()
    {
        player = PlayerSingleton.Instance.gameObject;
    }

    private void Update()
    {
        Vector3 lookDirection = transform.position - player.transform.position;
        transform.rotation = Quaternion.LookRotation(lookDirection.normalized, Vector3.up);
    }
}
