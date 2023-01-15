using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Data;

[CreateAssetMenu(fileName = "DialoguesDatabase", menuName = "ScriptableObjects/Dialogues")]
public class DialoguesSO : ScriptableObject
{
    public Dialogue[] dialogues;
}
