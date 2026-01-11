using System.Collections;
using UnityEngine;

public class MainMenuCanvas : MonoBehaviour
{
    [SerializeField] GameObject _game;

    public void QuitButton()
    {
        Application.Quit();
    }

    public void PlayButton()
    {
        StartCoroutine(Play());
    }

    private IEnumerator Play()
    {
        FindFirstObjectByType<ScreenFader>().FadeIn();
        yield return new WaitForSeconds(0.4f);

        _game.SetActive(true);

        FindFirstObjectByType<ScreenFader>().FadeOut();

        gameObject.SetActive(false);
    }
}
