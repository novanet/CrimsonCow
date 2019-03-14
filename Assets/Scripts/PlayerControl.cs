using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerControl : MonoBehaviour
{
    [Range(1,2)]
    public int PlayerNumber;

    [FormerlySerializedAs("Speed")] [Tooltip("Units per second")]
    public float MoveSpeed;
    [Tooltip("Degrees per second")]
    public float RotationSpeed = 180f;
    [Tooltip("Degrees per second")]
    public float PitchSpeed = 120f;
    
    void FixedUpdate()
    {
        Rotate();

        Pitch();
        
        Move();
    }

    private void Rotate()
    {
        var horizontal = Input.GetAxisRaw($"Horizontal.Player{PlayerNumber}");
        transform.Rotate(Vector3.back, RotationSpeed * Time.fixedDeltaTime * horizontal, Space.Self);
    }

    private void Pitch()
    {
        var vertical = Input.GetAxisRaw($"Vertical.Player{PlayerNumber}");
        transform.Rotate(Vector3.right, PitchSpeed * Time.fixedDeltaTime * vertical, Space.Self);
    }
    
    private void Move()
    {
        transform.position += transform.forward * (MoveSpeed * Time.deltaTime);
    }
}
