using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointTowardsGoal : MonoBehaviour
{
    private Vector3 _goal;

    private void Start()
    {
        _goal = new Vector3(0, 0, float.MaxValue);
    }

    void Update()
    {
        transform.LookAt(_goal);
    }
}
