using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coordinate
{
    public Vector3 position;
    public string tag;
    public bool isLampCeiling;
    public bool isEnemySpawn;

    public Coordinate(Vector3 position, string tag, bool isLampCeiling = false, bool isEnemySpawn = false)
    {
        this.position = position;
        this.tag = tag;
        this.isLampCeiling = isLampCeiling;
        this.isEnemySpawn = isEnemySpawn;
    }
}
