using System.Text;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEngine;

public class ImportDialogues : Editor
{
    [MenuItem("Tools/Import Dialogues CSV")]
    public static void DoSomething()
    {
        TextAsset csv = Resources.Load<TextAsset>("Dialogues");
        StringBuilder csvAsString = new StringBuilder(csv.text);
        DivideCsvByID(csvAsString);
    }

    static void DivideCsvByID(StringBuilder csvAsString)
    {
        string[] strings = csvAsString.ToString().Split("\n");
        foreach(string s in strings)
        {
            Debug.Log(s);
            Debug.Log(s.Substring(0, s.IndexOf(";")));
        }
    }
}
