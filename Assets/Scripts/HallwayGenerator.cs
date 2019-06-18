using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HallwayGenerator : MonoBehaviour
{
    public int NumberOfObstacles;
    public int NumberOfLeadingSegments;
    public GameObject HallwayPrefab;
    public GameObject GoalPrefab;
    public GameObject[] Obstacles;

    void Start()
    {
        var totalSegments = NumberOfObstacles + NumberOfLeadingSegments + 1; // one extra for goal segment
        var obstacles = CreateEndlessRandomObstacleList().Take(NumberOfObstacles).ToArray();
        
        var sizeOfLastSegment = new Vector3(75,75,75); // intend to be able to use segments of different sizes later
        
        for (var i = 0; i < totalSegments; i++)
        {
            var position = new Vector3(0, 0, sizeOfLastSegment.z * i);
            
            // create empty hallway segment
            var segment = Instantiate(HallwayPrefab, position, Quaternion.identity, transform);

            if (i == totalSegments - 1) // last element, let's make a goal
            {
                Instantiate(GoalPrefab, position, Quaternion.identity, transform);
            } 
            else if (i >= NumberOfLeadingSegments) // we're beyond the leading empty segment, so let's place an obstacle
            {
                Instantiate(obstacles[i - NumberOfLeadingSegments], position, Quaternion.identity, segment.transform);
            }
        }
    }

    private IEnumerable<GameObject> CreateEndlessRandomObstacleList()
    {
        var obstacles = new List<GameObject>();
        
        while (true)
        {
            if (!obstacles.Any())
                obstacles.AddRange(Obstacles);
            
            var random = Random.Range(0, obstacles.Count);
            var obstacle = obstacles[random];
            obstacles.Remove(obstacle);

            yield return obstacle;
        }
    }
}
