using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coordinate
{
    public Vector3 position;
    public string tag;
    public bool isEndPosition;
    public bool isEnemySpawn;

    public Coordinate(Vector3 position, string tag, bool isEndPosition = false, bool isEnemySpawn = false)
    {
        this.position = position;
        this.tag = tag;
        this.isEndPosition = isEndPosition;
        this.isEnemySpawn = isEnemySpawn;
    }
}
