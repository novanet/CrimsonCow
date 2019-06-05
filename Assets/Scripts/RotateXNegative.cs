using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateXNegative : MonoBehaviour
{
    [Tooltip("Degrees per second")]
    public float Speed = 60;
    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Speed * Time.deltaTime * -1, 0, 0);
    }
}
