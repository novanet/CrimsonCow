using UnityEngine;

public class ShatterOnImpact : MonoBehaviour
{
    public Transform Prefab;
    public int ShardsPerAxis = 6;
    
    public void OnTriggerEnter(Collider other)
    {
        var size = GetComponent<Renderer>().bounds.size;
        var shardSize = size / 6;
        var startX = shardSize.x / 2;
        var startY = shardSize.y / 2;
        
        var containerObject = new GameObject("ShatteredGoal").transform;
        containerObject.parent = transform.parent;
        
        for (int x = 0; x < ShardsPerAxis; x++)
        {
            for (int y = 0; y < ShardsPerAxis; y++)
            {
                var basePosition = transform.position - new Vector3(size.x / 2, size.y / 2, size.z / 2);
                var relativePosition = new Vector3(startX + shardSize.x * x, startY + shardSize.y * y, 0);
                CreateShard(basePosition + relativePosition, containerObject);
            }
        }
        
        Destroy(gameObject);
    }

    private void CreateShard(Vector3 position, Transform parent)
    {
        var shard = Instantiate(Prefab, position, Quaternion.identity, parent);
        var shardRigidbody = shard.GetComponent<Rigidbody>();
           
        // give random spin
        shardRigidbody.AddTorque(
            Random.Range(-90f, 90f),
            Random.Range(-90, 90f),
            Random.Range(-90, 90f)
            );
    }
}
