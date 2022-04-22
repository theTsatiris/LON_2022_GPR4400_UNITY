using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeightMap
{
    public float[,] map;

    public int width;
    public int height;
    public float scale;

    public float x_offset;
    public float y_offset;

    public void GenerateHeightMap(int width, int height, float scale, float x_offset, float y_offset)
    {
        this.width = width;
        this.height = height;
        this.scale = scale;
        this.x_offset = x_offset;
        this.y_offset = y_offset;

        this.map = new float[this.width, this.height];

        for(int x = 0; x < this.width; x++)
        {
            for(int y = 0; y < this.height; y++)
            {
                //RANDOM = BAD!!!
                //this.map[x, y] = Random.Range(0.0f, 1.0f);

                //PERLIN = GOOD!!
                this.map[x, y] = CalculatePerlinNoise(x, y);
            }
        }
    }

    float CalculatePerlinNoise(int x, int y)
    {
        float x_coord = ((float)x / width) * scale + x_offset; 
        float y_coord = ((float)y / height) * scale + y_offset;

        return Mathf.PerlinNoise(x_coord, y_coord);
    }
}
