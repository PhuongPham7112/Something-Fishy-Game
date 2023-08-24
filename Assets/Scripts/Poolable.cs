using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Poolable : MonoBehaviour
{
    public string poolTag;

    public void ReturnToPool()
    {
        ObjectPool.Instance.poolDictionary[poolTag].Enqueue(gameObject);
        gameObject.SetActive(false);
    }
}