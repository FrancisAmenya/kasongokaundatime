using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreCounter : MonoBehaviour
{

    public GameObject ScriptCarrier, GameSummaryObject;

    public int scoreValue;

    int activeScene, currentTime, CoinsToJarsCounter = 0, MoreCoinsCounter = 0, CoinsValueCounter = 0;

    int ImportedCoinsToJarsCounter = 6, ImportedMoreCoinsCounter = 3, ImportedCoinsValueCounter = 3;

    bool CoinsToJars, MoreCoins, CoinsValue;

    public bool levelIsComplete; // = false;

    TextMeshProUGUI scoreText;

    MenuScript menuScript;

    LevelComplete levelComplete;

    TimeCounter timeCounter;


    // Start is called before the first frame update
    private void Start()
    {
        
        levelIsComplete = false;

        activeScene = SceneManager.GetActiveScene().buildIndex;

        menuScript = ScriptCarrier.GetComponent<MenuScript>();

        scoreText = GetComponent<TextMeshProUGUI>();

        levelComplete = GameSummaryObject.GetComponent<LevelComplete>();

        
        if (SceneManager.GetActiveScene().Equals(SceneManager.GetSceneByBuildIndex(4)))//"Match-Coins-To-Jars"))
        {

            CoinsToJars = true;

            CoinsValue = MoreCoins = false;

        }

        else

        if (SceneManager.GetActiveScene().Equals(SceneManager.GetSceneByBuildIndex(5)))//"Which-Coins-Are-More"))
        {

            MoreCoins = true;

            CoinsToJars = CoinsValue = false;

        }

        else

        if (SceneManager.GetActiveScene().Equals(SceneManager.GetSceneByBuildIndex(6)))//"Whats-Is-The-Value"))
        {

            CoinsToJars = MoreCoins = false;

            CoinsValue = true;

        }

    }


    private void Update()
    {

        scoreText.text = "Score: " + scoreValue;

    }


    public void Match_Coins_To_Jars_Scores()
    {

        Debug.Log(scoreText.text + " ________________");

        //CoinsToJarsCounter++;

        if ( scoreValue >= 3000 || CoinsToJarsCounter == ImportedCoinsToJarsCounter || levelIsComplete )
        {

            GameSummaryObject.SetActive(true);

            levelComplete.DoLevelComplete(scoreValue, activeScene);

            Debug.Log(" ________________ activeScene is " + activeScene);

        }

    }


    public void Which_Coins_Are_More_Scores()
    {

        Debug.Log(scoreText.text + " ________________");

        MoreCoinsCounter++;

        if ( scoreValue >= 1500 || MoreCoinsCounter == ImportedMoreCoinsCounter || levelIsComplete)
        {

            GameSummaryObject.SetActive(true);

            levelComplete.DoLevelComplete(scoreValue, activeScene);

            Debug.Log(" ________________ activeScene is " + activeScene);

        }

    }


    public void Whats_The_Value_Of_The_Coins_Scores()
    {

        Debug.Log(scoreText.text + " ________________");

        CoinsValueCounter++;

        if ( scoreValue >= 1500 || CoinsValueCounter == ImportedCoinsValueCounter || levelIsComplete )
        {

            GameSummaryObject.SetActive(true);

            levelComplete.DoLevelComplete(scoreValue, activeScene);

            Debug.Log(" ________________ activeScene is " + activeScene);

        }

    }


    public void SetScore(int score, bool resetScore)
    {

        currentTime = (int)Time.time;

        if ( score > 0 && !resetScore )
        {

            scoreValue += score;

            Debug.Log(" scoreValue in scoreCounter SetScore is " + scoreValue);

        }

        else

        if ( score is 0 && !resetScore )
        {

            Debug.Log(" scoreValue in scoreCounter SetScore empty: score is 0 && !resetScore  ");

        }

        else

        if ( score is 0 && resetScore )
        {

            Debug.Log(" scoreValue in scoreCounter SetScore added by 0 ");

        }

        if (CoinsToJars)
        {

            Match_Coins_To_Jars_Scores();

        }

        else

        if (MoreCoins)
        {

            Which_Coins_Are_More_Scores();

        }

        else

        if (CoinsValue)
        {

            Whats_The_Value_Of_The_Coins_Scores();

        }

    }

}
