using UnityEngine;

public class PauseCanvas : MonoBehaviour
{
    [SerializeField] GameObject _canvas, _mmCanvas, _game;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) && !_mmCanvas.activeSelf) _canvas.SetActive(!_canvas.activeSelf);

        if (_canvas.activeSelf && !_mmCanvas.activeSelf) Time.timeScale = 0;
        else Time.timeScale = 1;
    }

    public void ResumeButton()
    {
        _canvas.SetActive(false);
    }

    public void MMButton()
    {
        _mmCanvas.SetActive(true);
        _game.SetActive(false);
        _canvas.SetActive(false);
    }
}
