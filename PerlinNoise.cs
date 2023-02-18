using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerlinNoise : MonoBehaviour {

    public int mapWidth;
    public int mapHeight;
    public float noiseScale;

    public int octaves;
    [Range(0,1)]
    public float persistance;
    public float lacunarity;

    public int seed;
    public Vector2 offset;

    void Start() {
        float[,] noiseMap = GenerateNoiseMap(mapWidth, mapHeight, noiseScale, octaves, persistance, lacunarity, seed, offset);

        for(int x = 0;x < mapWidth;x++)
        {
            for(int y = 0;y < mapHeight;y++)
            {
                print(noiseMap[x,y]);
            }
        }
    }

    

    float[,] GenerateNoiseMap(int width, int height, float scale, int octaves, float persistance, float lacunarity, int seed, Vector2 offset) {

    float[,] noiseMap = new float[width,height];

    System.Random prng = new System.Random(seed);
    Vector2[] octaveOffsets = new Vector2[octaves];
    for (int i = 0; i < octaves; i++) {
        float offsetX = prng.Next(-100000, 100000) + offset.x;
        float offsetY = prng.Next(-100000, 100000) + offset.y;
        octaveOffsets[i] = new Vector2(offsetX, offsetY);
    }

    if (scale <= 0) {
        scale = 0.0001f;
    }

    float maxNoiseHeight = float.MinValue;
    float minNoiseHeight = float.MaxValue;

    float halfWidth = width / 2f;
    float halfHeight = height / 2f;

    for (int y = 0; y < height; y++) {
        for (int x = 0; x < width; x++) {

            float amplitude = 1;
            float frequency = 1;
            float noiseHeight = 0;

            for (int i = 0; i < octaves; i++) {
                float sampleX = (x-halfWidth) / scale * frequency + octaveOffsets[i].x;
                float sampleY = (y-halfHeight) / scale * frequency + octaveOffsets[i].y;

                float perlinValue = Mathf.PerlinNoise(sampleX, sampleY) * 2 - 1;
                noiseHeight += perlinValue * amplitude;

                amplitude *= persistance;
                frequency *= lacunarity;
            }

            if (noiseHeight > maxNoiseHeight) {
                maxNoiseHeight = noiseHeight;
            } else if (noiseHeight < minNoiseHeight) {
                minNoiseHeight = noiseHeight;
            }
            noiseMap[x,y] = noiseHeight;
        }
    }

    for (int y = 0; y < height; y++) {
        for (int x = 0; x < width; x++) {
            noiseMap[x,y] = Mathf.InverseLerp(minNoiseHeight, maxNoiseHeight, noiseMap[x,y]);
        }
    }

    return noiseMap;
}

}