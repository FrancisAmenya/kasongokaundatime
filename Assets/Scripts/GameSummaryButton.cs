using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSummaryButton : MonoBehaviour
{

    public GameObject GameSummaryObject;

    LevelComplete levelComplete;

    // Use this for initialization
    void Start()
    {

        levelComplete = GameSummaryObject.GetComponent<LevelComplete>();

    }


    public void OnPointerDown()
    {

        levelComplete.ButtonHandler(gameObject);

    }

}
