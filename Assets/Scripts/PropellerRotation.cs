using UnityEngine;

public class PropellerRotation : MonoBehaviour
{
    public float RotationsPerSecond = 5f;
    
    void Update()
    {
        var angle = 360 * (RotationsPerSecond * Time.deltaTime);
        transform.Rotate(Vector3.forward, angle, Space.Self);
    }
}
