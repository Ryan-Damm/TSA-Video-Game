using UnityEngine;

public class EndLevel : MonoBehaviour
{
    PlayerMovement _pm;
    SpriteRenderer _sr;

    bool _open = false;

    private void Start()
    {
        _sr = GetComponent<SpriteRenderer>();
        _pm = FindFirstObjectByType<PlayerMovement>();  
    }

    private void Update()
    {
        if(_pm.keyCount == 3) _open = true;
        else _open = false;

        if(_open) _sr.color = Color.white;
        else _sr.color = Color.black;
    }
}
