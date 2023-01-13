using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialoguePlay : MonoBehaviour
{
    public DialogueID[] dialogues;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            DialogueManager.Instance.InitDialogue(dialogues);
        }
    }
}
