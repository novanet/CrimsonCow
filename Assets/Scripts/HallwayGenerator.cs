using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class HallwayGenerator : MonoBehaviour
{
    public int NumberOfObstacles;
    public int NumberOfLeadingSegments;
    public GameObject HallwayPrefab;
    public GameObject GoalPrefab;
    public GameObject[] Obstacles;
    private List<GameObject> _obstacles = new List<GameObject>();

    void Start()
    {
        _obstacles.AddRange(Obstacles);

        var totalSegments = NumberOfObstacles + NumberOfLeadingSegments;
        
        var sizeOfLastSegment = new Vector3(50, 50, 50);
        
        for (var i = 0; i < totalSegments; i++)
        {
            var position = new Vector3(0, 0, sizeOfLastSegment.z * i);
            var segment = Instantiate(HallwayPrefab, position, Quaternion.identity, transform);
            //sizeOfLastSegment = segment.GetComponent<Renderer>().bounds.size;

            if (i == totalSegments - 1)
            {
                Instantiate(GoalPrefab, position, Quaternion.identity, transform);
            } 
            else if (i >= NumberOfLeadingSegments)
            {
                PlaceRandomObstacle(position, segment.transform);
            }
                
        }
    }

    private void PlaceRandomObstacle(Vector3 position, Transform parent)
    {
        var random = Random.Range(0, _obstacles.Count);
        var obstacle = _obstacles[random];
        _obstacles.Remove(obstacle);

        Instantiate(obstacle, position, Quaternion.identity, parent);
    }
}
