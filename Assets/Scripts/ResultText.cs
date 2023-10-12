using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ResultText : MonoBehaviour
{
    private TextMeshProUGUI textMeshPro;
    // Start is called before the first frame update
    void Start()
    {
        textMeshPro = GetComponent<TextMeshProUGUI>();

        if (!GameState.isEscape)
        {
            textMeshPro.text = "Stranded before a way out";
        }
        else
        {
            textMeshPro.text = "You've escaped";
        }
    }
}
