using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    //private Scene SceneMainMenu;

    //private AssetBundle myLoadedAssetBundle;

    //private string[] scenePaths;

    string prevScene;

    public static bool resizableWindow;

    bool levelScene;

    // Start is called before the first frame update
    void Start()
    {

        resizableWindow = true;

        //SceneMainMenu = SceneManager.GetSceneByBuildIndex(2);
        //myLoadedAssetBundle = AssetBundle.LoadFromFile("Assets/AssetBundles/scenes");
        //scenePaths = myLoadedAssetBundle.GetAllScenePaths();

        levelScene = false;

        prevScene = PlayerPrefs.GetString("LastSceneNumber");

    }

    // Update is called once per frame

    //void OnGUI()
    //{
    //    if (GUI.Button(new Rect(10, 10, 100, 30), "Change Scene"))
    //    {
    //        Debug.Log("Scene2 loading: " + scenePaths[0]);
    //        SceneManager.LoadScene(scenePaths[0], LoadSceneMode.Single);
    //    }
    //}


    public void LoadStartMenu()
    {

        Time.timeScale = 1;

        //SceneManager.LoadScene(1);

        PlayerPrefs.SetInt("NextSceneNumber", 2);

        ChooseSceneIndex(2);

        //ChooseScene("whichCoinsAreMore");

    }


    public void LoadMainMenu()
    {

        Time.timeScale = 1;

        PlayerPrefs.SetInt("NextSceneNumber", 3);

        ChooseSceneIndex(3);

        //ChooseScene("whichCoinsAreMore");

    }


    public void LoadMatchCoinsToJars()
    {

        Debug.Log(" LoadMatchCoinsToJars() ");

        PlayerPrefs.SetInt("NextSceneNumber", 4);

        ChooseSceneIndex(4);

        //ChooseScene("matchCoinsToJars");

    }


    public void LoadWhichCoinsAreMore()
    {

        Debug.Log(" LoadWhichCoinsAreMore() ");

        PlayerPrefs.SetInt("NextSceneNumber", 5);

        ChooseSceneIndex(5);

        //ChooseScene("whichCoinsAreMore");

    }


    public void LoadWhatIsTheValue()
    {

        Debug.Log(" LoadWhatIsTheValue() ");

        PlayerPrefs.SetInt("NextSceneNumber", 6);

        ChooseSceneIndex(6);

        //ChooseScene("whatIsTheValue");

    }




    public void ChooseSceneIndex(int sceneIndex)
    {

        Debug.Log(" ChooseSceneIndex() " + sceneIndex);

        //StartCoroutine(LoadAsync(sceneIndex));

        SceneManager.LoadScene(sceneIndex);

        //SceneManager.LoadSceneAsync(SceneManager.GetSceneByBuildIndex(SceneIndex).name);

    }


/// <summary>
/// Start loading the specified scene (asynchronously).
/// </summary>
/// <param name="sceneName">Scene name.</param>
    public void ChooseScene(string sceneName)
    {

        //StartCoroutine(LoadAsync(sceneName));

        SceneManager.LoadScene(SceneManager.GetSceneByName(sceneName).name);

    }

    /// <summary>
    /// Coroutine to load the scene asynchronously, invoking the
    /// appropriate callbacks.
    /// </summary>
    ///// <param name="sceneName">Scene name.</param>
    //IEnumerator LoadAsync(int sceneIndex)//string sceneName)
    //{

    //    //AsyncOperation async = SceneManager.LoadSceneAsync(SceneManager.GetSceneByName(sceneName).name.Length > 0 ? sceneName : SceneMainMenu.name);
    //    if (!prevScene.Equals("splashScreen") || sceneIndex is 4 || sceneIndex is 5 || sceneIndex is 6)
    //    {

    //        async = SceneManager.LoadSceneAsync(sceneIndex);//SceneManager.GetSceneByBuildIndex(2).buildIndex is 2 ? 2 : sceneIndex);

    //        while (!async.isDone)
    //        {

    //            if (progress != null) progress.Invoke(async.progress);

    //            Debug.Log(" Loading.. " + async.progress);

    //            for (int i = 0; i < 8; i++)
    //            {

    //                yield return new WaitForEndOfFrame();

    //            }

    //        }

    //        if (complete != null) complete.Invoke();

    //    }

    //    else

    //    {
            
    //        SceneManager.LoadScene(sceneIndex);

    //    }

    //    PlayerPrefs.SetString("LastSceneNumber", SceneManager.GetActiveScene().name);

    //}
}
