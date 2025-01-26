using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;
using Image = UnityEngine.UI.Image;

public class LevelComplete : MonoBehaviour
{

    int bestScore, currentScore, pointsForMaxStars, activeSceneIndex;

    float timeValue, bestTime;

    bool scoreAnimationDone, timeAnimationDone;

    public TextMeshProUGUI titleText, pointsScoredText, 
        timeElapsedText, pointsBestText, timeBestText, scoreCounterText;

    public GameObject star1GameObject, star2GameObject, star3GameObject, ScriptCarrier, menuButton, replayButton, nextButton,
        scoreCounterObject;
        //titleObject, pointsScoredObject, timeElapsedObject, pointsBestObject, timeBestObject, timeCounterObject;

    public Image star1, star2, star3;

    MenuScript menuScript;

    public ScoreCounter scoreCounter;

    public Button next, menu, replay;

    public TimeCounter timeCounter;

    // Start is called before the first frame update
    void Start()
    {
        //timeValue = 0f;

        //bestScore = 0;

        //bestTime = 60f;

        //activeSceneIndex = 0;

        //pointsForMaxStars = 0;

        star1 = star1GameObject.GetComponent<Image>();

        star2 = star2GameObject.GetComponent<Image>();

        star3 = star3GameObject.GetComponent<Image>();

        menuScript = ScriptCarrier.GetComponent<MenuScript>();

        menu.interactable = false;

        replay.interactable = false;

        next.interactable = false;

        scoreAnimationDone = false;

        timeAnimationDone = false;

    }



    public void DoLevelComplete(int currentScoreImport, int activeScene)       
    {

        timeValue = timeCounter.GetStartingTime() - ( Mathf.Round( timeCounter.GetCurrentTime() * 100f) / 100f );

        scoreCounter.scoreValue = 0;

        Debug.Log("*********LevelComplete: timeValue is " + timeValue + " **********");

        timeCounter.timeIsLeft = false;

        activeSceneIndex = activeScene;

        currentScore = currentScoreImport;

        StartCoroutine(CountUpToScore(pointsScoredText, (float)currentScoreImport, 2f, 1f));


        //while (!scoreAnimationDone)
        //{

        //}

        //StartCoroutine(CountUpToTime(timeElapsedText, timeValue, 2f, 3f, " s"));

    }


    public void ButtonHandler(GameObject button)
    {

        if (button.Equals(menuButton))
        {

            menuScript.LoadMainMenu();

        }

        else

        if (button.Equals(replayButton))
        {

            switch (activeSceneIndex)
            {

                case 4:

                    menuScript.LoadMatchCoinsToJars();

                    break;

                case 5:

                    menuScript.LoadWhichCoinsAreMore();

                    break;

                case 6:

                    menuScript.LoadWhatIsTheValue();

                    break;

                default:

                    menuScript.LoadMainMenu();

                    break;

            }

        }

        else

        if (button.Equals(nextButton))
        {

            switch (activeSceneIndex)
            {

                case 4:

                    menuScript.LoadWhichCoinsAreMore();

                    break;

                case 5:

                    menuScript.LoadWhatIsTheValue();

                    break;

                case 6:

                    menuScript.LoadMainMenu();

                    break;

                default:

                    menuScript.LoadMainMenu();

                    break;

            }

        }

    }


    void FinishLevelComplete(int activeScene) 
    {

        switch (activeScene)
        {

            case 4:

                pointsForMaxStars = 3000;

                break;

            case 5:

                pointsForMaxStars = 1500;

                break;

            case 6:

                pointsForMaxStars = 1500;

                break;

            default:

                pointsForMaxStars = 1500;

                break;

        }

        Debug.Log("*********FinishLevelComplete: pointsForMaxStars assigned **********");


        //int bestScoreTemp = PlayerPrefs.GetInt("bestScore" + activeScene, 499);

        //float bestTimeTemp = PlayerPrefs.GetFloat("bestTime" + activeScene, 45f);

        //Debug.Log("*********FinishLevelComplete: all bestTemps fetched **********");


        bestScore = PlayerPrefs.GetInt("bestScore" + activeScene, 499);

        bestTime = PlayerPrefs.GetFloat("bestTime" + activeScene, 45f);

        Debug.Log("*********FinishLevelComplete: all bestTemps set **********");


        if (currentScore > bestScore)
        {

            PlayerPrefs.SetInt("bestScore" + activeScene, currentScore);

            bestScore = currentScore;

        }


        if (timeValue <= bestTime && currentScore >= pointsForMaxStars * 2 / 3)
        {

            PlayerPrefs.SetFloat("bestTime" + activeScene, timeValue);

            bestTime = timeValue;

        }


        Debug.Log("*********FinishLevelComplete: all highScores updated **********");


        pointsBestText.text = bestScore.ToString();

        timeBestText.text = bestTime.ToString("F2") + " s";

        Debug.Log("*********FinishLevelComplete: all highScores enabled **********");


        //reward stars after 2 sec
        StartCoroutine(WaitForStars(1f));

    }

    IEnumerator WaitForStars(float delay) 
    {
        
        yield return new WaitForSecondsRealtime(delay);

        if (currentScore > (pointsForMaxStars / 3))
        {
            star2GameObject.SetActive(true);
        }
        if (currentScore > (pointsForMaxStars * 2 / 3))
        {
            star3GameObject.SetActive(true);
        }
        if (currentScore > (pointsForMaxStars))
        {
            star1GameObject.SetActive(true);
        }

        Debug.Log("*********FinishLevelComplete: all Stars set **********");

        yield return new WaitForSecondsRealtime(delay * 2);

        menu.interactable = true;

        replay.interactable = true;

        next.interactable = true;

        Debug.Log("*********FinishLevelComplete: all buttons enabled **********");

    }

    IEnumerator CountUpToScore(TextMeshProUGUI label, float targetVal, float duration, float delay)
    {
        if (delay > 0f)
        {
            yield return new WaitForSecondsRealtime(delay);
        }

        int current = 0;

        int finalTargetVal = (int)targetVal;

        while (current < targetVal)
        {
            // step by amount that will get us to the target value within the duration
            current += (int)(targetVal / (duration / Time.deltaTime));

            Debug.Log("LevelComplete + current (casted) is " + current);

            current = Mathf.Clamp(current, 0, finalTargetVal);

            Debug.Log("LevelComplete + current (Clamped) is " + current);

            label.text = current.ToString();

            Debug.Log("LevelComplete + Score label is " + label.text);

            yield return new WaitForEndOfFrame();

        }

        //set displayed value to exact as parsed, removes (.01) error possibility
        label.text = finalTargetVal.ToString();

        scoreAnimationDone = true;

        StartCoroutine(CountUpToTime(timeElapsedText, timeValue, 2f, 1f, " s"));

        Debug.Log("LevelComplete IEnumerator CountUpToScore done, StartCoroutine(CountUpToTime(timeElapsedText, timeValue, 2f, 3f, \" s\")) is, " +
             timeElapsedText + ", " + timeValue);

        //FinishLevelComplete(activeSceneIndex);

        //Debug.Log("LevelComplete IEnumerator CountUpToScore done, FinishLevelComplete(activeSceneIndex) is " + activeSceneIndex);

        Debug.Log("LevelComplete IEnumerator CountUpToScore done is ");

    }


    IEnumerator CountUpToTime(TextMeshProUGUI label, float targetVal, float duration, float delay, string prefix)
    {
        if (delay > 0f)
        {
            yield return new WaitForSecondsRealtime(delay);
        }

        float current = 0f;

        float finalTargetVal = (int)( targetVal * 100f ) / 100f;

        while (current < targetVal)
        {
            // step by amount that will get us to the target value within the duration
            current += (targetVal / (duration / Time.deltaTime));

            Debug.Log("LevelComplete + current (casted) is " + current);

            current = Mathf.Clamp(current, 0, targetVal);

            Debug.Log("LevelComplete + current (Clamped) is " + current);

            label.text = (int)(current * 100f ) / 100f + prefix;

            Debug.Log("LevelComplete + Time label is " + label.text);

            yield return new WaitForEndOfFrame();

        }

        //set displayed value to exact as parsed, removes (.01) error possibility
        label.text = finalTargetVal.ToString() + prefix;

        yield return new WaitForSecondsRealtime(delay);
        
        FinishLevelComplete(activeSceneIndex);

        Debug.Log("LevelComplete IEnumerator CountUpToTime done, FinishLevelComplete(activeSceneIndex) is " + activeSceneIndex);

        Debug.Log("LevelComplete IEnumerator CountUpToTime done is ");

    }

}
