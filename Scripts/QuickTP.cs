using UnityEngine;

public class QuickTP : MonoBehaviour
{
    [SerializeField] Transform _placeToPutPlayer;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.transform.position = _placeToPutPlayer.transform.position;
        }
    }
}
