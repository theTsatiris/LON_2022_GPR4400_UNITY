using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    HeightMap heightMap;

    [SerializeField]
    int width;

    [SerializeField]
    int height;

    [SerializeField]
    float perlinScale;

    [SerializeField]
    float x_offset;

    [SerializeField]
    float y_offset;

    [SerializeField]
    GameObject cloud;

    [SerializeField]
    float threshold;

    [SerializeField]
    float scaleFactor;

    [SerializeField]
    float noiseFactor;

    // Start is called before the first frame update
    void Start()
    {
        heightMap = new HeightMap();

        x_offset = Random.Range(0.0f, 9999.0f);
        y_offset = Random.Range(0.0f, 9999.0f);

        heightMap.GenerateHeightMap(width, height, perlinScale, x_offset, y_offset);

        for(int x = 0; x < width; x++)
        {
            for(int y = 0; y < height; y++)
            {
                //if heightmap value at x,y is greater than a threshold, then SPAWN!!!!
                if(heightMap.map[x,y] >= threshold)
                {
                    //calculating our own scale factor by re-scaling the heightmap value from [thres ... 1] to [0 ... 1]
                    //and multiplying by a scale factor to have some leverage.
                    float localScale = heightMap.map[x, y] - threshold;
                    localScale *= (1.0f / (1.0f - threshold));
                    localScale *= scaleFactor;

                    //Introduce a random displacement to "nudge" the gameobject off the grid
                    float dispX = Random.Range(-noiseFactor, noiseFactor);
                    float dispY = Random.Range(-noiseFactor, noiseFactor);
                    float dispZ = Random.Range(-noiseFactor, noiseFactor);

                    GameObject newCloud = Instantiate(cloud, new Vector3(x + dispX, dispY, y + dispZ), Quaternion.identity);
                    newCloud.transform.localScale = new Vector3(localScale, localScale, localScale);
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
