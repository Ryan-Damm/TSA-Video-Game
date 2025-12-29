using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] Transform pos1, pos2;
    [SerializeField] float speed = 2;

    Vector3 targetPos;

    private void Start()
    {
        transform.position = pos1.position;
        targetPos = pos2.position;
    }

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);

        if(Vector2.Distance(transform.position, targetPos) < 0.01f)
        {
            if(targetPos == pos1.position) { targetPos = pos2.position; }
            if(targetPos == pos2.position) { targetPos = pos1.position; }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            collision.collider.transform.SetParent(transform);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            collision.collider.transform.SetParent(null);
        }
    }
}
