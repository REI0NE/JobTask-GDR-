using UnityEngine;

#if UNITY_EDITOR
#endif

[RequireComponent(typeof(SpawnRange))]
public class Spawner : MonoBehaviour
{
    [SerializeField] private Component _component;
    [SerializeField] private Transform _parent;
    [SerializeField] private int _count;
    [SerializeField] private LayerMask _layerMask;
    private SpawnRange _spawnRange;

    private void Awake()
    {
        _spawnRange = GetComponent<SpawnRange>();
        Spawn(_component, _parent, _count);
    }

    private void Spawn(Component component, Transform transform, int count)
    {
        for (int i = 0; i < count; i++)
        {
            Component newComponent = Instantiate(component, transform);
            CircleCollider2D circleCollider2D = newComponent.GetComponent<CircleCollider2D>();

            while (!CheckCollision(circleCollider2D))
                newComponent.transform.position = GetRandomPosition();
        }
    }

    private Vector3 GetRandomPosition()
    {
        Vector3 position = new Vector2();
        position.x = Random.Range(-_spawnRange.Range.x / 2, _spawnRange.Range.x / 2);
        position.y = Random.Range(-_spawnRange.Range.y / 2, _spawnRange.Range.y / 2);
        return position;
    }

    private bool CheckCollision(CircleCollider2D circleCollider2D)
    {
        Physics2D.SyncTransforms();

        //Vector2 size = new Vector2(circleCollider2D.bounds.size.x, circleCollider2D.bounds.size.y);
        //float radius = ((size.x * size.x) / (8 * size.y) + size.y / 2);
        float radius = circleCollider2D.radius * circleCollider2D.transform.localScale.magnitude;
        int colliders = Physics2D.OverlapCircleAll(circleCollider2D.transform.position, radius, _layerMask).Length;

        return colliders <= 1;
    }
}