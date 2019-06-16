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
    private SlideInWinnerText _winnerText;
    private int _playerNumber;

    public void Start()
    {
        _winnerText = GameObject.Find("WinnerText").GetComponent<SlideInWinnerText>();
        _playerNumber = GetComponent<PlayerControl>().PlayerNumber;
        
        _thingsToDisable.Add(GetComponent<PlayerControl>());
        _thingsToDisable.Add(Camera.GetComponent<AutoCam>());
        
        _thingsToEnable.Add(Camera.GetComponent<LookAtCam>());
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Finish"))
        {
            //disable both players' input, enable gravity
            DisableAlmostEverything();
            OtherPlayer.DisableAlmostEverything();
            
            // let screen of winner take full screen
            Camera.GetComponent<Wipe>().GoFullScreen();
            OtherPlayer.Camera.GetComponent<Wipe>().GoAway();
            
            // slide in winner text, play cheering sound
            _winnerText.SetWinner(_playerNumber);
            
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