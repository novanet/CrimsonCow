using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BobGentlyUpAndDown : MonoBehaviour
{
    private int _direction = 1;
    private float _velocity = 0;
    private float _lastDirectionSwitch;
    public float ChangePerSecond = 0.5f;
    public float SecondsPerDirectionSwitch = 1f;
    private Vector3 _initialRotation;

    public void Start()
    {
        _initialRotation = transform.rotation.eulerAngles;
        _lastDirectionSwitch = Time.time - SecondsPerDirectionSwitch / 2;
    }
    
    public void Update()
    {
        if (IsTimeToChangeDirection())
        {
            _direction = _direction * -1;
            _lastDirectionSwitch = Time.time;
        }

        _velocity += (ChangePerSecond * Time.deltaTime) * _direction;

        transform.position = transform.position + new Vector3(0, _velocity, 0);
        transform.rotation = Quaternion.Euler( _initialRotation.x - _velocity * 100, _initialRotation.y, _initialRotation.z);
    }

    private bool IsTimeToChangeDirection()
    {
        return Time.time > _lastDirectionSwitch + SecondsPerDirectionSwitch;
    }
}
