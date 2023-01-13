using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance;

    [SerializeField] private GameObject dialogueBox;
    [SerializeField] private Text nameText;
    [SerializeField] private Image persoImageLeft, persoImageRight;
    [SerializeField] private GameObject interactionImage;
    [SerializeField] private Text dialogueText;

    public delegate void DialogueDelegate();
    private DialogueDelegate actualDialogueDelegate = null;

    //Cree une liste ranger dans l ordre d apparition les objets present
    public Queue<string> sentences = new Queue<string>();
    public Queue<string> names = new Queue<string>();
    public Queue<Sprite> sprites = new Queue<Sprite>();
    public Queue<Pos> pos = new Queue<Pos>();
    private string actualSentence;
    private string playerName;

    private DialogueState actualDialogueState;
    [SerializeField] private float cdAction, cdDialogue;

    private ReadGoogleSheet readGoogleSheet;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;

        readGoogleSheet = GetComponent<ReadGoogleSheet>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (actualDialogueDelegate != null)
            {
                actualDialogueDelegate();
            }
        }
    }

    public void InitDialogue(DialogueID[] dialogue)
    {
        actualDialogueState = DialogueState.INTERACTION;
        actualDialogueDelegate = DisplayTextInstant;

        persoImageLeft.sprite = dialogue[0].persoSprite;
        persoImageRight.sprite = dialogue[1].persoSprite;

        readGoogleSheet.GetTextString(dialogue);
    }

    public void StartDialogue(string[] dialogue, Sprite[] perso, string[] name, Pos[] allPos)
    {
        sentences.Clear();
        names.Clear();
        sprites.Clear();
        pos.Clear();

        dialogueBox.SetActive(true);

        for (int i = 0; i < dialogue.Length; i++)
        {
            sentences.Enqueue(dialogue[i]);
            names.Enqueue(name[i]);
            sprites.Enqueue(perso[i]);
            pos.Enqueue(allPos[i]);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        interactionImage.SetActive(false);

        if(sentences.Count == 0)
        {
            dialogueBox.SetActive(false);
            actualDialogueDelegate = null;
            return;
        }

        actualDialogueDelegate = DisplayTextInstant;

        actualSentence = sentences.Dequeue();
        string actualName = names.Dequeue();
        Sprite actualSprite = sprites.Dequeue();
        Pos actualPos = pos.Dequeue();

        StopAllCoroutines();
        StartCoroutine(TypeSentence(actualSentence, actualSprite, actualName, actualPos));
    }

    private IEnumerator TypeSentence(string sentence, Sprite sprite, string name, Pos actualPos)
    {

        if (actualPos == Pos.LEFT)
        {
            persoImageLeft.color = Color.white;
            persoImageRight.color = Color.gray;
        }
        else
        {
            persoImageLeft.color = Color.gray;
            persoImageRight.color = Color.white;
        }

        nameText.text = name;

        dialogueText.text = "";
        foreach(char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(0.01f);
        }

        //TEXTE FINIS D ETRE ECRIS

        actualDialogueDelegate = DisplayNextSentence;
        interactionImage.SetActive(true);

    }

    public void DisplayTextInstant()
    {
        if (actualDialogueState == DialogueState.INTERACTION || actualDialogueState == DialogueState.INTERACTION_ACTION)
        {
            StopAllCoroutines();
            dialogueText.text = actualSentence;
            interactionImage.SetActive(true);
            actualDialogueDelegate = DisplayNextSentence;
        }
    }
}
