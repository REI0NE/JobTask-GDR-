using UnityEngine;

public class SpawnRange : MonoBehaviour
{
    [SerializeField] private Vector2 _range;
    public Vector2 Range { get => _range; set => _range = value; }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, _range);
    }
}
