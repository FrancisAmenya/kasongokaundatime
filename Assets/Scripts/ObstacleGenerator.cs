using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This class handles constant generation of obstacles on screen
public class ObstacleGenerator : MonoBehaviour
{
    private float elapsedTime, nextGenerationTime;

    [Header("References ---")]
    public Transform cactusSpawnPoint;
    public Transform birdSpawnPoint;
    public GameObject cactusGameObject;
    public GameObject birdGameObject;

    [Header("Variables ---")]
    public float minGenerationTime;
    public float maxGenerationTime;

    //Update is called once per frame
    void Update()
    {
        //Increase elapsed time each second
        elapsedTime += Time.deltaTime;

        //If elapsed time is greater than next spawn time, generate an obstacle
        if (elapsedTime >= nextGenerationTime)
        {
            elapsedTime = 0;
            int random = Random.Range(0, 4); //Get a random number to determine which obstacle to generate

            if (random == 0) //If it's 0, generate a bird
            {
                GameObject bird = Instantiate(birdGameObject, birdSpawnPoint.position, transform.rotation);
                Destroy(bird, 4);
            }
            else //If it's not 0, generate a cactus
            {
                GameObject cactus = Instantiate(cactusGameObject, cactusSpawnPoint.position, transform.rotation);
                Destroy(cactus, 4);
            }

            //When we generate an obstacle, get a random time for the next obstacle generation
            nextGenerationTime = Random.Range(minGenerationTime, maxGenerationTime);
        }
    }
}
