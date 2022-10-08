using UnityEngine;

public class CameraArea : MonoBehaviour
{
    [SerializeField] private SpawnRange _spawnRange;
    [SerializeField] private Vector2 _offset;

    private void Start() => transform.localScale = _offset + _spawnRange.Range;
}
