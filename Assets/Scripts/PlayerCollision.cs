using System;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    private PlayerControl _playerControl;
    private bool _isStunned = false;
    private float _timeOfLastCollision = 0;
    private Vector3 _backoffTarget;
    private float _cameraDistance;
    
    [Tooltip("Number of seconds the player will lose control on collision")]
    public float StunTime = 1f;
    [Tooltip("Number of units the player will be sent backwards on collision")]
    public float BackOffDistance = 2f;
    [Tooltip("Player camera")]
    public Camera Camera;

    public void Start()
    {
        _playerControl = GetComponent<PlayerControl>();
    }

    public void Update()
    {
        if (_isStunned && (Time.time - _timeOfLastCollision > StunTime))
        {
            Unstun();
            EnablePlayerControl();
        }
        else if (_isStunned)
        {
            BackOff();
            SetCamera();
        }
    }

    private void BackOff()
    {
        var distance = ((transform.forward * -BackOffDistance) / StunTime) * Time.deltaTime;
        
        transform.position += distance;
    }
    
    private void SetCamera()
    {
        Camera.transform.position = transform.position - transform.forward * _cameraDistance;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            return;
        
        Debug.Log($"OnTriggerEnter: {other.name} ({other.GetType().Name})");

        Stun();
        DisablePlayerControl();
        SetTarget();
        RegisterCameraDistance();
    }

    private void Stun()
    {
        _isStunned = true;
        _timeOfLastCollision = Time.time;
    }

    private void Unstun()
    {
        _isStunned = false;
    }

    private void DisablePlayerControl()
    {
        _playerControl.enabled = false;
    }

    private void EnablePlayerControl()
    {
        _playerControl.enabled = true;
    }

    private void SetTarget()
    {
        _backoffTarget = transform.position - transform.forward * BackOffDistance;
    }

    private void RegisterCameraDistance()
    {
        _cameraDistance = (transform.position - Camera.transform.position).magnitude;
    }
}
