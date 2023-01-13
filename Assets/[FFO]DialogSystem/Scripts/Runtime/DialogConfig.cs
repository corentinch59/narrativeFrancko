using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogConfig : MonoBehaviour
{
    [System.Serializable]
    public struct SpeakerConfig
    {
        public enum POSITION
        {
            LEFT,
            MIDDLE,
            RIGHT
        }
        public POSITION position;
        public SpeakerDatabase speakerDatabase;
        public SpeakerData speakerData;

        public void SetPosition(POSITION newPosition)
        {
            this.position = newPosition;
        }
    }

    [System.Serializable]
    public struct SentenceConfig
    {
        public SpeakerData speakerData;
        [TextArea]public string sentence;
        public AudioClip audioClip;
    }

    public List<SpeakerDatabase> speakerDatabases = new();

    public List<SpeakerConfig> speakers = new();

    public List<SentenceConfig> sentenceConfig = new();

}
