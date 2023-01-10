using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using Unity.VisualScripting;
using Sequence = DG.Tweening.Sequence;

public class TextTweenTest : MonoBehaviour
{
    public TextMeshProUGUI myText;
    public string[] wordsToHighlight;

    private void Start()
    {
        DOTweenTMPAnimator animator = new DOTweenTMPAnimator(myText);
        Sequence sequence = DOTween.Sequence();

        for (int i = 0; i < animator.textInfo.characterCount; ++i)
        {
            animator.DOFadeChar(i, 0f, 0f);
        }
        
        for (int i = 0; i < animator.textInfo.characterCount; ++i)
        {
            if (!animator.textInfo.characterInfo[i].isVisible) continue;
            sequence.Append(animator.DOFadeChar(i, 1f, 0.1f));
        }

        sequence.onComplete += () => { Debug.Log("Sequence finished !"); };
    }

    private int[] GetIndexWord(int numberOfWords)
    {
        int[] woa = new int[numberOfWords];

        return woa;
    }
}
