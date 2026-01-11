using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    Rigidbody2D _rb;

    [SerializeField] Transform pos1, pos2;
    [SerializeField] float speed = 2;

    Vector2 lastPos;
    Vector3 targetPos;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        _rb.position = pos1.position;
        targetPos = pos2.position;
        lastPos = _rb.position;
    }

    private void FixedUpdate()
    {
        lastPos = _rb.position;

        Vector2 newPos = Vector2.MoveTowards(
            _rb.position,
            targetPos,
            speed * Time.fixedDeltaTime
        );

        _rb.MovePosition(newPos);

        if (Vector2.Distance(_rb.position, targetPos) < 0.05f)
        {
            targetPos = (targetPos == (Vector3)pos1.position)
                ? pos2.position
                : pos1.position;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Vector2 platformDelta = _rb.position - lastPos;
            collision.rigidbody.position += platformDelta;
        }
    }
}
