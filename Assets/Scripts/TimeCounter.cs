using TMPro;
using UnityEngine;

public class TimeCounter : MonoBehaviour
{

    float timeValue = 61f;

    float startingTime;

    int timeInt;

    public TextMeshProUGUI timeText;

    public bool timeIsLeft;

    public GameObject scoreCounterObject; //ScriptCarrier,

    ScoreCounter scoreCounter;

    //MenuScript menuScript;

    // Start is called before the first frame update
    void Start()
    {

        startingTime = timeValue;

        string timeTextStr = timeText.text;

        string timeValueStr = timeValue.ToString();

        scoreCounter = scoreCounterObject.GetComponent<ScoreCounter>();

        //menuScript = ScriptCarrier.GetComponent<MenuScript>();

        timeText = GetComponent<TextMeshProUGUI>();

        timeIsLeft = true;

        if (timeTextStr != timeValueStr)
        {

            timeText.text = timeValueStr;

        }
        //StartCoroutine ( "CountDownTimer" );
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if (timeIsLeft)
        {

            CountDownTimerB();

        }

    }

    public void CountDownTimerB()
    {

        timeValue -= Time.deltaTime;

        timeInt = (int)(timeValue);

        timeText.text = "TIME: " + timeInt + " sec";

        if (timeValue > 0)
        {

            Debug.Log("time left is " + timeText.text);

        }

        else
        
        {

            timeIsLeft = false;

            Debug.Log("No more time left");
            //show summary UI here
            //go to level completed/ failed menu?

            scoreCounter.levelIsComplete = true;

            scoreCounter.SetScore(0, true);

            //menuScript.LoadMainMenu();
            
        }

    }

    /*
     IEnumerator CountDownTimer ()
    {
        timeText.text = "TIME: " + timeValue + "s";
        Debug.Log ( timeText.text );
        yield return new WaitForSeconds ( 1 );
        timeValue-- ;

        if ( timeValue == 0 )
        {
            //yield return null;
        }
    }
    */
    public float GetCurrentTime()
    {

        return timeValue;

    }


    public float GetStartingTime()
    {

        return startingTime;

    }

}
