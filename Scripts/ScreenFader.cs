using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ScreenFader : MonoBehaviour
{
    [SerializeField] private Image _screenFade;
    public float fadeTime = 1f;

    void Start()
    {
        _screenFade.color = new Color(0, 0, 0, 1);
        FadeOut();
    }

    public void FadeOut()
    {
        StartCoroutine(Fade(1, 0));
    }

    public void FadeIn()
    {
        StartCoroutine(Fade(0, 1));
    }

    private IEnumerator Fade(float startAlpha, float endAlpha)
    {
        float elapsedTime = 0;
        Color startColor = _screenFade.color;
        Color endColor = new Color(startColor.r, startColor.g, startColor.b, endAlpha);

        while (elapsedTime < fadeTime)
        {
            _screenFade.color = Color.Lerp(startColor, endColor, elapsedTime / fadeTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        _screenFade.color = endColor;
    }
}
