using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Rooms
{
    public static void Generate(Vector3 start_position)
    {
        int size_quad = UnityEngine.Random.Range(4, 10);
        //Log(start_position);
        Vector3 build_position = start_position;
        build_position.x = start_position.x - Mathf.Floor(size_quad / 2) * 4;
        build_position.z = start_position.z - Mathf.Floor(size_quad / 2) * 4;
        for (int i = 0; i < size_quad; i++)
        {
            build_position.x = start_position.x;
            for (int j = 0; j < size_quad; j++)
            {
                build_position.x += 4;
                Generation.Instance.coordinates.Add(new Coordinate(build_position,"room"));
            }
            build_position.z += 4;
        }
    }
}