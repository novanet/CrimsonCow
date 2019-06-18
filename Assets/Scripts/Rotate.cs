using UnityEngine;

public class Rotate : MonoBehaviour
{
    [Tooltip("Degrees per second")]
    public float Speed = 60;
    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 0, Speed * Time.deltaTime);
    }
}
