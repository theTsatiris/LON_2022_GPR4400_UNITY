using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainGenerator : MonoBehaviour
{
    Terrain terrain;
    HeightMap heightMap;

    [SerializeField]
    int width;

    [SerializeField]
    int height;

    [SerializeField]
    int depth;

    [SerializeField]
    float scale;

    [SerializeField]
    float x_offset;

    [SerializeField]
    float y_offset;

    // Start is called before the first frame update
    void Start()
    {
        terrain = GetComponent<Terrain>();
        heightMap = new HeightMap();

        x_offset = Random.Range(0.0f, 9999.0f);
        y_offset = Random.Range(0.0f, 9999.0f);
    }

    // Update is called once per frame
    void Update()
    {
        heightMap.GenerateHeightMap(width, height, scale, x_offset, y_offset);
        terrain.terrainData = GenerateTerrainData(terrain.terrainData);
    }

    TerrainData GenerateTerrainData(TerrainData td)
    {
        td.heightmapResolution = width + 1;
        td.size = new Vector3(height, depth, width);
        td.SetHeights(0, 0, heightMap.map);

        return td;
    }

}
