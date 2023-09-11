using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LetterContent : MonoBehaviour
{
    Item item;
    TextMeshPro textMeshPro;
    void Start()
    {
        Highlight highlightComponent = GetComponentInParent<Highlight>();
        item = highlightComponent.item;
        textMeshPro = GetComponent<TextMeshPro>();
        textMeshPro.text = item.readableContent;
    }
}
