using UnityEngine;

namespace Data
{
    public enum CHARACTER
    {
        PLAYER,
        AURORA,
        GUARD
    }

    public enum EMOTION
    {
        EXCITED,
        HAPPY,
        NEUTRAL,
        SAD,
        ANGRY,
        MAD
    }

    [System.Serializable]
    public struct DialogueLine
    {
        public string name;
        public CHARACTER character;
        public EMOTION emotion;
        public string line;
    }

    [System.Serializable]
    public class Dialogue
    {
        public string name;
        public string id;
        public DialogueLine[] lines;
        [HideInInspector] public bool hasBeenUsed;
    }
}
