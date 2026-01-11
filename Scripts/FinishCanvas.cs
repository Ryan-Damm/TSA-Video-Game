using TMPro;
using UnityEngine;

public class FinishCanvas : MonoBehaviour
{
    GameObject _mmCanvas;

    private void Awake()
    {
        _mmCanvas = FindFirstObjectByType<MainMenuCanvas>().gameObject;
    }

    public void MMButton()
    {
        _mmCanvas.SetActive(true);
        gameObject.SetActive(false);
    }
}
