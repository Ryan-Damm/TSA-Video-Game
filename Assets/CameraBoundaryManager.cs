using Unity.Cinemachine;
using UnityEngine;

public class CameraBoundaryManager : MonoBehaviour
{
    [SerializeField] private CinemachineConfiner2D _confiner;
    [SerializeField] private PolygonCollider2D[] _chunkConfiners;
    [SerializeField] private Transform _player;

    private PolygonCollider2D _lastConfiner;

    void Update()
    {
        if (_player == null) return;

        Vector3 playerPosition = _player.position;
        PolygonCollider2D currentChunkCollider = GetChunkColliderForPosition(playerPosition);

        if (currentChunkCollider != null && currentChunkCollider != _lastConfiner)
        {
            _confiner.BoundingShape2D = currentChunkCollider;
            _confiner.InvalidateBoundingShapeCache();
            _lastConfiner = currentChunkCollider;
        }
    }

    private PolygonCollider2D GetChunkColliderForPosition(Vector3 position)
    {
        foreach (var collider in _chunkConfiners)
        {
            if (collider != null && collider.enabled && collider.OverlapPoint(position))
                return collider;
        }

        return null;
    }
}
