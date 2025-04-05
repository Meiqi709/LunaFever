using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TextFadeAnimation : MonoBehaviour
{
    public float flashInterval = 0.8f;

    private TextMeshProUGUI textMeshPro;
    private Image image;

    private void Start()
    {
        textMeshPro = GetComponent<TextMeshProUGUI>();
        image = GetComponent<Image>(); 

        if (textMeshPro == null && image == null)
        {
            return;
        }

        StartCoroutine(FadeTextCoroutine());
    }

    private IEnumerator FadeTextCoroutine()
    {
        while (true)
        {
            yield return StartCoroutine(FadeTo(0f, flashInterval));
            yield return StartCoroutine(FadeTo(1f, flashInterval));
        }
    }

    private IEnumerator FadeTo(float targetAlpha, float duration)
    {
        float time = 0f;
        float startTextAlpha = textMeshPro != null ? textMeshPro.alpha : 0f;
        float startImageAlpha = image != null ? image.color.a : 0f;

        while (time < duration)
        {
            time += Time.deltaTime;
            float lerpAlpha = Mathf.Lerp(startTextAlpha, targetAlpha, time / duration);

            if (textMeshPro != null)
                textMeshPro.alpha = lerpAlpha;

            if (image != null)
            {
                Color currentColor = image.color;
                currentColor.a = lerpAlpha;
                image.color = currentColor;
            }

            yield return null;
        }

        if (textMeshPro != null)
            textMeshPro.alpha = targetAlpha;

        if (image != null)
        {
            Color finalColor = image.color;
            finalColor.a = targetAlpha;
            image.color = finalColor;
        }
    }
}
