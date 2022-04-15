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

    // Start is called before the first frame update
    void Start()
    {
        terrain = GetComponent<Terrain>();
        heightMap = new HeightMap();
    }

    // Update is called once per frame
    void Update()
    {
        heightMap.GenerateHeightMap(width, height, scale);
        terrain.terrainData = GenerateTerrainData(terrain.terrainData);
    }

    TerrainData GenerateTerrainData(TerrainData td)
    {
        td.heightmapResolution = width + 1;
        td.size = new Vector3(width, depth, height);
        td.SetHeights(0, 0, heightMap.map);

        return td;
    }

}
