using UnityEngine;

public class LookAtCam : MonoBehaviour
{
    public Transform Target;
    
    void Update()
    {
        transform.LookAt(Target);
    }
}
