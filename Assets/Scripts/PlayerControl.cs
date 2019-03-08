using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerControl : MonoBehaviour
{
    [FormerlySerializedAs("Speed")] [Tooltip("Units per second")]
    public float MoveSpeed;
    [Tooltip("Degrees per second")]
    public float RotationSpeed = 180f;
    [Tooltip("Degrees per second")]
    public float PitchSpeed = 120f;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Rotate();

        Pitch();
        
        Move();
    }

    private void Rotate()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Rotate(Vector3.forward, RotationSpeed * Time.fixedDeltaTime, Space.Self);
        }
        
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Rotate(Vector3.forward, -RotationSpeed * Time.fixedDeltaTime, Space.Self);
        }
    }

    private void Pitch()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            transform.Rotate(Vector3.right, PitchSpeed * Time.fixedDeltaTime, Space.Self);
        }        

        if (Input.GetKey(KeyCode.DownArrow))
        {
            transform.Rotate(Vector3.right, -PitchSpeed * Time.fixedDeltaTime, Space.Self);
        }        
    }
    
    private void Move()
    {
        transform.position += transform.forward * (MoveSpeed * Time.deltaTime);
    }
}
