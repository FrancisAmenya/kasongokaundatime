using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunStateManager : MonoBehaviour
{
    [Header("Sun Prefab References")]
    [SerializeField] private GameObject sunRagePrefab;
    [SerializeField] private GameObject sunHappyPrefab;
    [SerializeField] private GameObject sunCryingPrefab;
    [SerializeField] private GameObject sunSmilingPrefab;
    [SerializeField] private GameObject sunSleepingPrefab;
    [SerializeField] private GameObject sunSadPrefab;

    private GameObject sunRage;
    private GameObject sunHappy;
    private GameObject sunCrying;
    private GameObject sunSmiling;
    private GameObject sunSleeping;
    private GameObject sunSad;

    private void Start()
    {
        InstantiateSunPrefabs();
        SunIsSmiling(); // Set default state
        Debug.Log("Sun States initialized");  // Debugging line
    }

    private void InstantiateSunPrefabs()
    {
    // Set spawn position in front of the camera
    Vector3 spawnPosition = Camera.main.transform.position + new Vector3(6, 2, 5);
        
        sunRage = Instantiate(sunRagePrefab, spawnPosition, Quaternion.identity);
        sunHappy = Instantiate(sunHappyPrefab, spawnPosition, Quaternion.identity);
        sunCrying = Instantiate(sunCryingPrefab, spawnPosition, Quaternion.identity);
        sunSmiling = Instantiate(sunSmilingPrefab, spawnPosition, Quaternion.identity);
        sunSleeping = Instantiate(sunSleepingPrefab, spawnPosition, Quaternion.identity);
        sunSad = Instantiate(sunSadPrefab, spawnPosition, Quaternion.identity);

        // Make them children of this object for better organization
        sunRage.transform.SetParent(transform);
        sunHappy.transform.SetParent(transform);
        sunCrying.transform.SetParent(transform);
        sunSmiling.transform.SetParent(transform);
        sunSleeping.transform.SetParent(transform);
        sunSad.transform.SetParent(transform);
    }
    
    public void SunIsSmiling()
    {
        sunRage.SetActive(false);
        sunHappy.SetActive(false);
        sunCrying.SetActive(false);
        sunSmiling.SetActive(true);
        sunSleeping.SetActive(false);
        sunSad.SetActive(false);
    }

    public void SunIsRaging()    
    {
        sunRage.SetActive(true);
        sunHappy.SetActive(false);
        sunCrying.SetActive(false);
        sunSmiling.SetActive(false);
        sunSleeping.SetActive(false);
        sunSad.SetActive(false);
    }

    public void SunIsCrying()
    {
        sunRage.SetActive(false);
        sunHappy.SetActive(false);
        sunCrying.SetActive(true);
        sunSmiling.SetActive(false);
        sunSleeping.SetActive(false);
        sunSad.SetActive(false);
    }

    public void SunIsSleeping()   
    {
        sunRage.SetActive(false);
        sunHappy.SetActive(false);
        sunCrying.SetActive(false);
        sunSmiling.SetActive(false);
        sunSleeping.SetActive(true);
        sunSad.SetActive(false);
    }

    public void SunIsSad()  
    {
        sunRage.SetActive(false);
        sunHappy.SetActive(false);
        sunCrying.SetActive(false);
        sunSmiling.SetActive(false);
        sunSleeping.SetActive(false);
        sunSad.SetActive(true);
    }

    public void SunIsHappy()    
    {
        sunRage.SetActive(false);
        sunHappy.SetActive(true);
        sunCrying.SetActive(false);
        sunSmiling.SetActive(false);
        sunSleeping.SetActive(false);
        sunSad.SetActive(false);
    }
   
}
