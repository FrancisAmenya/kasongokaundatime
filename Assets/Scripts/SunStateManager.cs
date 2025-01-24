using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunStateManager : MonoBehaviour
{
    public GameObject sunRage;
    public GameObject sunHappy;
    public GameObject sunCrying;
    public GameObject sunSmiling;
    public GameObject sunSleeping;
    public GameObject sunSad;
    // Start is called before the first frame update
    void Start()
    {
        
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
