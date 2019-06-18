using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointTowardsGoal : MonoBehaviour
{
    private Vector3 _goal;
    
    public Camera Camera;

    public void Start()
    {
        _goal = new Vector3(0, 0, float.MaxValue);
    }

    public void Update()
    {
        transform.localRotation = Quaternion.Euler(-Camera.transform.rotation.eulerAngles);
    }
}
