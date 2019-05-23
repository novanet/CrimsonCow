﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShatterOnImpact : MonoBehaviour
{
    public Material Material;
    public int ShardsPerAxis = 6;
    
    public void OnCollisionEnter(Collision other)
    {
        var size = GetComponent<Renderer>().bounds.size;
        var shardSize = size / 6;
        Debug.Log($"I am a cube of size {shardSize.x},{shardSize.y},{shardSize.z}");
        var startX = shardSize.x / 2;
        var startY = shardSize.y / 2;
        
        var containerObject = new GameObject("ShatteredGoal").transform;
        containerObject.parent = transform.parent;
        
        for (int x = 0; x < ShardsPerAxis; x++)
        {
            for (int y = 0; y < ShardsPerAxis; y++)
            {
                var basePosition = transform.position - new Vector3(size.x / 2, size.y / 2, size.z / 2);
                var relativePosition = new Vector3(startX + shardSize.x * x, startY + shardSize.y * y, 0);
                CreateShard(shardSize, basePosition + relativePosition, containerObject);
            }
        }
        
        Destroy(gameObject);
    }

    private void CreateShard(Vector3 size, Vector3 position, Transform parent)
    {
        size = size * 0.9f;
        
        var shard = GameObject.CreatePrimitive(PrimitiveType.Cube);
        shard.transform.parent = parent;
        
        shard.transform.localScale = size;
        shard.transform.position = position;
        shard.GetComponent<Renderer>().material = Material;
        
        //add physics
        var rigidBody = shard.AddComponent<Rigidbody>();
        
        // give random spin
        rigidBody.AddTorque(
            Random.Range(-90f, 90f),
            Random.Range(-90, 90f),
            Random.Range(-90, 90f)
            );
    }
}
