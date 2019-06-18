using UnityEngine;

public class RandomStartRotation : MonoBehaviour
{
    void Start()
    {
        transform.Rotate(transform.forward, Random.Range(0, 4) * 90);
    }
}
