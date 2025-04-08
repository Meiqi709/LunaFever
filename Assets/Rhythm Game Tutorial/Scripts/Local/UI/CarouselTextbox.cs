using TMPro;
using UnityEngine;
using System.Collections;

public class CarouselTextbox : MonoBehaviour 
{   
    [SerializeField] private TMP_Text headline;

    [SerializeField] private bool fadeText = true;
    private float _fadeDuration = 0.5f;
    private float _halfFadeDuration => _fadeDuration * 0.5f;

    private Coroutine _fadeCoroutine;

    public void SetTextWihoutFade(string headlineText)
    {
        headline.SetText(headlineText);

        headline.alpha = 1;

    }

    public void SetText(string headLineText, float fadingDuration = 0f)
{
    if (!fadeText || fadingDuration <= 0)
    {
        SetTextWihoutFade(headLineText);
        return;
    }

    if (_fadeCoroutine != null)
    {
        StopCoroutine(_fadeCoroutine);
        
        headline.alpha = 1;
    }

    _fadeDuration = fadingDuration;
    _fadeCoroutine = StartCoroutine(FadeText(headLineText));
}


private IEnumerator FadeText(string headLineText)
{
    float time = 0;
    while (time < _halfFadeDuration)
    {
        time += Time.deltaTime;
        float lerpValue = 1 - (time / _halfFadeDuration);
        headline.alpha = lerpValue;
        yield return null;
    }

    headline.SetText(headLineText);

    time = 0;
    while (time < _halfFadeDuration)
    {
        time += Time.deltaTime;
        float lerpValue = time / _halfFadeDuration;
        headline.alpha = lerpValue;
        yield return null;
    }
}



}

