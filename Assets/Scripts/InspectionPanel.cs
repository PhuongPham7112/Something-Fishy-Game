using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InspectionPanel : MonoBehaviour
{
    public TextMeshProUGUI description;

    public void ChangeText(string newText)
    {
        if (description != null)
        {
            description.text = newText;
        }
    }

    public void ChangePanelVisibility(bool change)
    {
        gameObject.SetActive(change);
    }
}
