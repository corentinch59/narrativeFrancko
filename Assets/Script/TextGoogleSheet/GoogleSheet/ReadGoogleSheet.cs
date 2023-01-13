using SimpleJSON;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using static System.Net.WebRequestMethods;

public class ReadGoogleSheet : MonoBehaviour
{
    [Obsolete]
    public void GetTextString(DialogueID[] dialogueId)
    {
        StartCoroutine(ObtainSheetData(dialogueId));
    }

    [Obsolete]
    IEnumerator ObtainSheetData(DialogueID[] dialogueId)
    {
        string link = "https://sheets.googleapis.com/v4/spreadsheets/11HeIxoXTcmEcJ2bSRSySPyQMftcyPENFiX6AVqqaDWY/values/Feuille%201?key=AIzaSyBLAdauLnxGZsp9wHva5rStJJZzq6cdUls";
        UnityWebRequest www = UnityWebRequest.Get(link);
        yield return www.SendWebRequest();
        if (www.isNetworkError || www.isHttpError || www.timeout > 2)
        {
            Debug.Log("Error" + www.error);
        }
        else
        {
            string json = www.downloadHandler.text;
            var o = JSON.Parse(json);

            string[] sentence = new string[dialogueId.Length];
            string[] names = new string[dialogueId.Length];
            Sprite[] sprites = new Sprite[dialogueId.Length];
            Pos[] allpos = new Pos[dialogueId.Length];
            for (int i = 0; i < dialogueId.Length; i++)
            {
                sentence[i] = JSON.Parse(o["values"][dialogueId[i].lineId][dialogueId[i].columnId].ToString());
                names[i] = dialogueId[i].name;
                sprites[i] = dialogueId[i].persoSprite;
                allpos[i] = dialogueId[i].pos;
            }
            DialogueManager.Instance.StartDialogue(sentence, sprites, names, allpos);
        }
    }
}
