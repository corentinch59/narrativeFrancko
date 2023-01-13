using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogController : MonoBehaviour
{
    public Text txtNameLeft;
    public Image imgSpriteLeft;

    public Text txtNameRight;
    public Image imgSpriteRight;

    public Text txtSentence;

    private DialogConfig _dialog;
    private int _idCurrentSentence = 0;

    public GameObject dialogueScreen;

    private AudioSource _audioSource;

    private void Awake()
    {
        //_audioSource = GetComponent<AudioSource>();
    }

    public void PlayDialog(DialogConfig dialog)
    {
        dialogueScreen.SetActive(true);

        txtNameLeft.text = dialog.speakers[0].speakerData.label;
        imgSpriteLeft.sprite = dialog.speakers[0].speakerData.statuses[0].icon;

        txtNameRight.text = dialog.speakers[1].speakerData.label;
        imgSpriteRight.sprite = dialog.speakers[1].speakerData.statuses[0].icon;
        
        _dialog = dialog;

        RefreshBox();
    }

    private void RefreshBox()
    {
        DialogConfig.SentenceConfig sentence = _dialog.sentenceConfig[_idCurrentSentence];

        DialogConfig.SpeakerConfig speaker = _dialog.speakers[0];

        switch (speaker.position)
        {
            case DialogConfig.SpeakerConfig.POSITION.LEFT:
                txtNameLeft.color = Color.black;
                txtNameRight.color = Color.clear;
                
                imgSpriteLeft.color = Color.white;
                imgSpriteRight.color = Color.gray;
                break;

            case DialogConfig.SpeakerConfig.POSITION.RIGHT:
                txtNameLeft.color = Color.clear;
                txtNameRight.color = Color.black;

                imgSpriteLeft.color = Color.gray;
                imgSpriteRight.color = Color.white;
                break;
        }

        txtSentence.text = sentence.sentence;

        //_audioSource.Stop();
        
        //_audioSource.clip = sentence.audioClip;
        //_audioSource.Play();
    }

    public void NextSentence()
    {
        _idCurrentSentence++;

        if (_idCurrentSentence < _dialog.sentenceConfig.Count) 
            RefreshBox();
        else
            CloseDialog();
    }

    public void CloseDialog()
    {
        _idCurrentSentence = 0;
        _dialog = null;

        gameObject.SetActive(false);
    }
}
