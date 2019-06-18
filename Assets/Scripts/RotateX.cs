using UnityEngine;

public class RotateX : MonoBehaviour
{
    [Tooltip("Degrees per second")]
    public float Speed = 60;
    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Speed * Time.deltaTime, 0, 0);
    }
}
