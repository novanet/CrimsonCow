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
            // ceiling
            CreateWall(
                new Vector3(Width, 1, Depth),
                new Vector3(0, Height / 2 + WallThickness / 2, Depth * i));
            
            // floor
            CreateWall(
                new Vector3(Width, 1, Depth),
                new Vector3(0, - Height / 2 - WallThickness / 2, Depth * i));

            // right wall
            CreateWall(
                new Vector3(1, Height, Depth),
                new Vector3(Width / 2 + WallThickness / 2, 0, Depth * i));

            // left wall
            CreateWall(
                new Vector3(1, Height, Depth),
                new Vector3(- Width / 2 - WallThickness / 2, 0, Depth * i));
        }
    }

    private void CreateWall(Vector3 size, Vector3 position)
    {
        var cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        cube.transform.localScale = size;
        cube.transform.position = position;
    }

    void Update()
    {
        
    }
}
