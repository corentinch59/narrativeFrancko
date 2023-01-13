using Mono.Cecil;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.Networking.Types;
using static DialogConfig;

[CustomEditor(typeof(DialogConfig))]
[CanEditMultipleObjects]
public class DialogConfigEditor : Editor
{
    private DialogConfig _source;


    private GUIStyle _titleStyle;

    private void OnEnable()
    {
        _source = target as DialogConfig;
    }

    #region INSPECTOR
    public override void OnInspectorGUI()
    {
        InitStyle();
        DrawSpeakersDatabasePanel();

        EditorGUILayout.Space();

        EditorGUI.BeginDisabledGroup(_source.speakerDatabases.Count == 0 || _source.speakerDatabases.Exists( x => x == null));
        
        DrawSpeakersPanel();
        DrawSentencesPanel();

        EditorGUI.EndDisabledGroup();

    }

    private void DrawSpeakersDatabasePanel()
    {
        EditorGUILayout.BeginVertical("box");

        DrawHeader();
        DrawBody();
        DrawFooter();

        EditorGUILayout.EndVertical();

        void DrawHeader()
        {
            EditorGUILayout.BeginHorizontal();

            EditorGUILayout.LabelField("Speakers Database", _titleStyle);
            if (GUILayout.Button(new GUIContent("X", "Clear all Database"), GUILayout.Width(30)))
            {
                if (EditorUtility.DisplayDialog("Delete all database", "Do you want delete all speakers database?", "Yes", "No"))
                    _source.speakerDatabases.Clear();
            }

            EditorGUILayout.EndHorizontal();
        }
        void DrawBody()
        {
            if (_source.speakerDatabases.Count != 0)
            {
                for (int i = 0; i < _source.speakerDatabases.Count; i++)
                {
                    EditorGUILayout.BeginHorizontal();
                    _source.speakerDatabases[i] = EditorGUILayout.ObjectField(_source.speakerDatabases[i], typeof(SpeakerDatabase), false) as SpeakerDatabase ;

                    if (GUILayout.Button(new GUIContent("X", "Remove database"), GUILayout.Width(30)))
                    {
                        _source.speakerDatabases.RemoveAt(i);
                        break;
                    }

                    EditorGUILayout.EndHorizontal();
                }
            }
        }
        void DrawFooter()
        {
            if (GUILayout.Button(new GUIContent("Add new database", "")))
            {
                _source.speakerDatabases.Add(null);
            }
        }
    }
    
    private void DrawSpeakersPanel()
    {
        EditorGUILayout.BeginVertical("box");

        DrawHeader();
        DrawBody();
        DrawFooter();

        EditorGUILayout.EndVertical();

        void DrawHeader()
        {
            EditorGUILayout.BeginHorizontal();

            EditorGUILayout.LabelField("Speakers", _titleStyle);
            if (GUILayout.Button(new GUIContent("X", "Clear all speakers"), GUILayout.Width(30)))
            {
                if(EditorUtility.DisplayDialog("Delete all speakers", "Do you want delete all speakers ?", "Yes", "No"))
                    _source.speakers.Clear();
            }

            EditorGUILayout.EndHorizontal();
        }
        void DrawBody()
        {
            if (_source.speakers.Count != 0)
            {
                for (int i = 0; i < _source.speakers.Count; i++)
                {
                    SpeakerConfig config = _source.speakers[i];

                    EditorGUILayout.BeginHorizontal();

                    if (_source.speakerDatabases.Count != 0)
                    {
                        if (_source.speakerDatabases.Count > 1)
                        {
                            List<string> alldatabaseLabel = new();
                            foreach (SpeakerDatabase sd in _source.speakerDatabases)
                                alldatabaseLabel.Add(sd?.name);

                            int idDatabate = _source.speakerDatabases.FindIndex(x => x == config.speakerDatabase);

                            idDatabate = EditorGUILayout.Popup(idDatabate < 0 ? 0 : idDatabate, alldatabaseLabel.ToArray());

                            config.speakerDatabase = _source.speakerDatabases[idDatabate];
                        }
                        else
                        {
                            config.speakerDatabase = _source.speakerDatabases.First();
                        }
                    }

                    if (config.speakerDatabase != null)
                    {
                        List<string> alldataLabel = new();
                        foreach (SpeakerData sd in config.speakerDatabase.speakerDatas)
                            alldataLabel.Add(sd?.label);

                        int idData = config.speakerDatabase.speakerDatas.FindIndex(x => x == config.speakerData);
                        
                        idData = EditorGUILayout.Popup(idData < 0 ? 0 : idData, alldataLabel.ToArray());

                        config.speakerData = config.speakerDatabase.speakerDatas[idData];
                    }

                    config.position = (SpeakerConfig.POSITION)EditorGUILayout.EnumPopup(config.position);

                    if (GUILayout.Button(new GUIContent("X", "Remove speeker"), GUILayout.Width(30)))
                    {
                        _source.speakers.RemoveAt(i);
                        break;
                    }

                    EditorGUILayout.EndHorizontal();

                    _source.speakers[i] = config;
                }
            }
        }
        void DrawFooter()
        {
            if (GUILayout.Button(new GUIContent("Add new speaker", "")))
            {
                _source.speakers.Add(new DialogConfig.SpeakerConfig());
            }
        }
    }

    private void DrawSentencesPanel()
    {
        EditorGUILayout.BeginVertical("box");

        DrawHeader();
        DrawBody();
        DrawFooter();

        void DrawHeader()
        {
            EditorGUILayout.BeginHorizontal();

            EditorGUILayout.LabelField("Sentences", _titleStyle);
            if (GUILayout.Button(new GUIContent("X", "Clear all sentences"), GUILayout.Width(30)))
            {
                if (EditorUtility.DisplayDialog("Delete all sentences", "Do you want delete all sentences ?", "Yes", "No"))
                    _source.sentenceConfig.Clear();
            }

            EditorGUILayout.EndHorizontal();
        }
        void DrawBody()
        {
            if (_source.sentenceConfig.Count != 0)
            {
                for (int i = 0; i < _source.sentenceConfig.Count; i++)
                {
                    SentenceConfig config = _source.sentenceConfig[i];

                    EditorGUILayout.BeginHorizontal();

                    if (_source.sentenceConfig.Count != 0)
                    {
                        if (_source.sentenceConfig.Count > 1)
                        {
                            List<string> alldatabaseLabel = new();
                            foreach (SpeakerDatabase sd in _source.speakerDatabases)
                                alldatabaseLabel.Add(sd?.name);

                            int idDatabate = _source.sentenceConfig.FindIndex(x => x.sentence == config.sentence);

                            idDatabate = EditorGUILayout.Popup(idDatabate < 0 ? 0 : idDatabate, alldatabaseLabel.ToArray());

                            config.sentence = _source.sentenceConfig[idDatabate].sentence;
                        }
                        else
                        {
                            config.sentence = _source.sentenceConfig.First().sentence;
                        }
                    }

                    if (_source.speakers != null)
                    {
                        List<string> alldataLabel = new();
                        foreach (SpeakerConfig sd in _source.speakers)
                            alldataLabel.Add(sd.speakerData.label);

                        int idData = _source.speakers.FindIndex(x => x.speakerData == config.speakerData);

                        idData = EditorGUILayout.Popup(idData < 0 ? 0 : idData, alldataLabel.ToArray());

                        config.speakerData = _source.sentenceConfig[idData].speakerData;
                    }


                    if (GUILayout.Button(new GUIContent("X", "Remove speeker"), GUILayout.Width(30)))
                    {
                        _source.sentenceConfig.RemoveAt(i);
                        break;
                    }

                    EditorGUILayout.EndHorizontal();
                    config.sentence = EditorGUILayout.TextArea(config.sentence, GUILayout.Height(50));

                    _source.sentenceConfig[i] = config;
                }
            }
        }
        void DrawFooter()
        {
            if (GUILayout.Button(new GUIContent("Add new sentence", "")))
            {
                _source.sentenceConfig.Add(new DialogConfig.SentenceConfig());
            }
        }
        EditorGUILayout.EndVertical();
    }

    #endregion

    #region STYLE
    private void InitStyle()
    {
        _titleStyle = GUI.skin.label;
        _titleStyle.alignment = TextAnchor.MiddleCenter;
        _titleStyle.fontStyle = FontStyle.Bold;
    }
    #endregion
}
