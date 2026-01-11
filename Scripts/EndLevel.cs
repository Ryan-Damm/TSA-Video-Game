using UnityEngine;

public class EndLevel : MonoBehaviour
{
    PlayerMovement _pm;
    SpriteRenderer _sr;
    BoxCollider2D _bc;

    [SerializeField] GameObject _game;

    bool _open = false;

    private void Start()
    {
        _sr = GetComponent<SpriteRenderer>();
        _pm = FindFirstObjectByType<PlayerMovement>();  
        _bc = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        if(_pm.keyCount == 3) _open = true;
        else _open = false;

        if(_open) { _sr.color =  Color.white; _bc.isTrigger = true; }
        else { _sr.color = Color.black; _bc.isTrigger = false; }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            FindFirstObjectByType<FinishCanvas>().gameObject.SetActive(true);
            _game.SetActive(false);
        }
    }
}
