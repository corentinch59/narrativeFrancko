using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialoguePlay : MonoBehaviour
{
    public DialogueID[] dialogues;

    public void StartDialogue()
    {
        DialogueManager.Instance.InitDialogue(dialogues);
    }
}
