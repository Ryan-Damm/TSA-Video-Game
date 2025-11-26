using System.Collections;
using UnityEngine;

public class ChangeRooms : MonoBehaviour
{
    [SerializeField] ScreenFader _screenFader;

    [SerializeField] float _loadingTime = 0f;

    [SerializeField] Transform _placeToPutPlayer;

    private void TransitionToNewArea(GameObject player)
    {
        _screenFader.FadeIn(); // Start the fade out

        //Move the player from a certain place to another to get across the gap

        StartCoroutine(TransitionCoroutine(player));
    }

    private IEnumerator TransitionCoroutine(GameObject player)
    {
        player.GetComponent<PlayerMovement>().canMove = false;
        yield return new WaitForSeconds(_screenFader.fadeTime); // Wait for fade to finish
        player.transform.position = _placeToPutPlayer.transform.position;
        yield return new WaitForSeconds(_loadingTime); // Simulate loading time

        _screenFader.FadeOut(); // Fade in once all else is done
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
