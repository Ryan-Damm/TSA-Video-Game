using System.Collections;
using UnityEngine;

public class ChangeRooms : MonoBehaviour
{
    [SerializeField] ScreenFader _screenFader;

    [SerializeField] float _loadingTime = 0f;

    [SerializeField] Transform _placeToPutPlayer;

    private void TransitionToNewArea(GameObject player)
    {
        _screenFader.FadeIn();

        StartCoroutine(TransitionCoroutine(player));
    }

    private IEnumerator TransitionCoroutine(GameObject player)
    {
        player.GetComponent<PlayerMovement>().canMove = false;
        yield return new WaitForSeconds(_screenFader.fadeTime); 
        player.transform.position = _placeToPutPlayer.transform.position;
        yield return new WaitForSeconds(_loadingTime);

        _screenFader.FadeOut();
        player.GetComponent<PlayerMovement>().canMove = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            TransitionToNewArea(collision.gameObject);
        }
    }
}
