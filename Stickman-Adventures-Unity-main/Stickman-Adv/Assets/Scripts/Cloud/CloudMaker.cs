using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudMaker : MonoBehaviour
{
    [Header("Cloud bounds")]
    public GameObject startPoint;
    public GameObject endPoint;
    [Header("Cloud prefabs")]
    public GameObject[] clouds;
    [Header("Spawn Settings")]
    public float spawnIntervalMin;
    public float spawnIntervalMax;    

    [Header("Speed Settings")]
    public float minCloudsSpeed;
    public float maxCloudSpeed;

    [Header("Scale Settings")]
    public float minCloudScale;
    public float maxCloudScale;

    [Header("Spawn Position Settings")]
    public int minStartClouds;
    public int maxStartClouds;
    [Header("Random Height Spawn Variation Settings")]
    public float heightVariationFactor;

    void Start()
    {
        preSpawnCloud();
        StartCoroutine("GenerateClouds");
    }

    IEnumerator GenerateClouds()
    {
        for(;;)
        {
            float nextSpawnTime = UnityEngine.Random.Range(spawnIntervalMin, spawnIntervalMax);
            SpawnCloud();
            
            yield return new WaitForSeconds(nextSpawnTime);
        }
    }

    private void SpawnCloud(float? xPosition = null)
    {
        int cloudIndex = UnityEngine.Random.Range(0, clouds.Length);
        GameObject cloud = Instantiate(clouds[cloudIndex]);
        
        float cloudSpeed = UnityEngine.Random.Range(minCloudsSpeed, maxCloudSpeed);
        float cloudScale = UnityEngine.Random.Range(minCloudScale, maxCloudScale);

        float cloudXPosition = xPosition ?? startPoint.transform.position.x;
        float cloudYposition = UnityEngine.Random.Range(startPoint.transform.position.y - heightVariationFactor, startPoint.transform.position.y + heightVariationFactor);
        
        float cloudOpacity = UnityEngine.Random.Range(0.4f, 1.0f);

        cloud.GetComponent<CloudBehavior>().Move(speed: cloudSpeed, deadEndX: endPoint.transform.position.x, scale: cloudScale, xPosition: cloudXPosition, yPosition: cloudYposition, alpha: cloudOpacity);        
    }

    private void preSpawnCloud() {
        int numberOfClouds = UnityEngine.Random.Range(minStartClouds, maxStartClouds);
        float xStart = startPoint.transform.position.x;
        float xFinish = endPoint.transform.position.x;

        for (int i = 0; i < numberOfClouds; i++)
        {
            float xPosition = UnityEngine.Random.Range(xStart, xFinish);
            SpawnCloud(xPosition);
        }
    }

}
