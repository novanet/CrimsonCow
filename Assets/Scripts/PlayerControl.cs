using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class PlayerControl : MonoBehaviour
{
    [Range(1,2)]
    public int PlayerNumber;

    [Tooltip("Increase in units per second")]
    public float Acceleration = 12f;

    public float MaxSpeed = 30f;
    [Tooltip("Degrees per second")]
    public float RotationSpeed = 180f;
    [Tooltip("Degrees per second")]
    public float PitchSpeed = 120f;

    private float _currentSpeed = 0f;
    private Rigidbody _rigidbody;
    private GameObject _rotationHelper;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _rotationHelper = new GameObject("RotationHelper");
        
        Physics.IgnoreLayerCollision(9, 10);
    }
    
    void FixedUpdate()
    {
        Rotate();

        Pitch();

        ApplyRotation();
        
        Move();
    }

    private void Rotate()
    {
        var horizontal = Input.GetAxisRaw($"Horizontal.Player{PlayerNumber}");
        _rotationHelper.transform.Rotate(Vector3.back, RotationSpeed * Time.fixedDeltaTime * horizontal, Space.Self);
    }

    private void Pitch()
    {
        var vertical = Input.GetAxisRaw($"Vertical.Player{PlayerNumber}");
        _rotationHelper.transform.Rotate(Vector3.right, PitchSpeed * Time.fixedDeltaTime * vertical, Space.Self);
    }
    
    private void ApplyRotation()
    {
        _rigidbody.MoveRotation(_rotationHelper.transform.rotation);
    }

    private void Move()
    {
        _currentSpeed += Acceleration * Time.deltaTime;
        if (_currentSpeed > MaxSpeed)
            _currentSpeed = MaxSpeed;

        _rigidbody.velocity = transform.forward * _currentSpeed;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Finish"))
            return;
        
        Debug.Log($"PlayerControl.OnCollisionEnter: {other.gameObject.name}");

        _currentSpeed = MaxSpeed / 3;
    }
}
