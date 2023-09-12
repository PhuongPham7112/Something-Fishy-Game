using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConvoTrigger : MonoBehaviour
{
    public List<string> currDialogue;
    DialogueBox dialogueBox;

    // Start is called before the first frame update
    void Start()
    {
        dialogueBox = DialogueBox.Instance;
    }

    public void PlayDialogue()
    {
        dialogueBox.SetText(currDialogue);
        dialogueBox.StartDialogue();
    }
}
