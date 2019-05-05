using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HallwayGenerator : MonoBehaviour
{
    public float Width, Height, Depth, WallThickness;
    public int NumberOfSegments;

    void Start()
    {
        for (var i = 0; i < NumberOfSegments; i++)
        {
            CreateEmptySegment($"Segment{i}", new Vector3(0, 0, Depth * i));
        }
    }

    private void CreateEmptySegment(string name, Vector3 position)
    {
        // Create empty gameObject per segment
        var segment = new GameObject(name).transform;
        segment.parent = transform;
            
        // ceiling
        CreateWall(
            new Vector3(Width, 1, Depth),
            new Vector3(0, Height / 2 + WallThickness / 2, 0) + position,
            segment);
            
        // floor
        CreateWall(
            new Vector3(Width, 1, Depth),
            new Vector3(0, - Height / 2 - WallThickness / 2, 0) + position,
            segment);

        // right wall
        CreateWall(
            new Vector3(1, Height, Depth),
            new Vector3(Width / 2 + WallThickness / 2, 0, 0) + position,
            segment);

        // left wall
        CreateWall(
            new Vector3(1, Height, Depth),
            new Vector3(- Width / 2 - WallThickness / 2, 0, 0) + position,
            segment);
    }

    private void CreateWall(Vector3 size, Vector3 position, Transform parent)
    {
        var cube = GameObject.CreatePrimitive(PrimitiveType.Cube).transform;
        cube.gameObject.name = "Wall";
        
        cube.localScale = size;
        cube.position = position;
        cube.parent = parent;
    }
}
