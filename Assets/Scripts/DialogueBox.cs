using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueBox : MonoBehaviour
{
    public static DialogueBox Instance { get; private set; }
    public List<string> currDialogue;
    private int index = 0;
    TextMeshProUGUI textMeshPro;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        textMeshPro = GetComponent<TextMeshProUGUI>();
        StartDialogue();
    }

    public void SetText(List<string> dialogue)
    {
        currDialogue = new List<string>(dialogue);
    }

    public void StartDialogue()
    {
        index = 0;
        StartCoroutine(RunDialogue());
    }

    IEnumerator RunDialogue()
    {
        while (index < currDialogue.Count)
        {
            textMeshPro.text = currDialogue[index];
            index++;
            yield return new WaitForSecondsRealtime(1.5f);
        }

        // Clear the text after the dialogue finishes
        textMeshPro.text = "";
    }
}
