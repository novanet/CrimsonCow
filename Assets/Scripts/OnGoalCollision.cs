using System;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Cameras;

public class OnGoalCollision : MonoBehaviour
{
    private List<MonoBehaviour> _thingsToDisable = new List<MonoBehaviour>();
    private List<MonoBehaviour> _thingsToEnable = new List<MonoBehaviour>();

    public Camera Camera;
    public OnGoalCollision OtherPlayer;
    
    public void Start()
    {
        _thingsToDisable.Add(GetComponent<PlayerControl>());
        _thingsToDisable.Add(Camera.GetComponent<AutoCam>());
        
        _thingsToEnable.Add(Camera.GetComponent<LookAtCam>());
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Finish"))
        {
            DisableAlmostEverything();
            OtherPlayer.DisableAlmostEverything();
            Destroy(this);
        }
    }

    public void DisableAlmostEverything()
    {
        _thingsToDisable.ForEach(x => x.enabled = false);        
        _thingsToEnable.ForEach(x => x.enabled = true);        
        
        var rigidbody = GetComponent<Rigidbody>();
        rigidbody.useGravity = true;
        rigidbody.isKinematic = false;
    }
}