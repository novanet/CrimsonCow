using System;
using UnityEngine;
using UnityStandardAssets.Cameras;

public class PlayerCollision : MonoBehaviour
{
    private PlayerControl _playerControl;
    private bool _isStunned = false;
    private float _timeOfLastCollision = 0;
    
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
        }
    }

    private void BackOff()
    {
        var distance = ((transform.forward * -BackOffDistance) / StunTime) * Time.deltaTime;
        
        transform.position += distance;
    }
    

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            return;

        if (other.CompareTag("Finish"))
        {
            TriggerEnd();
            return;
        }
        
        Debug.Log($"OnTriggerEnter: {other.name} ({other.GetType().Name})");

        PlaySound();
        Stun();
        DisablePlayerControl();
    }

    private void PlaySound()
    {
        System.Random rnd = new System.Random();
        int idx = rnd.Next(0, 2);
        var audioSources = GetComponents<AudioSource>();
        audioSources[idx].Play();
    }

    private void TriggerEnd()
    {
        Camera.GetComponent<AutoCam>().enabled = false;
        Camera.GetComponent<LookAtCam>().enabled = true;
        GetComponent<PlayerControl>().enabled = false;
        var rigidbody = GetComponent<Rigidbody>();
        rigidbody.useGravity = true;
        rigidbody.isKinematic = false;
        
        Destroy(this);
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
}
