using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Playgame : MonoBehaviour
{
    public void PlayGame()
    {

        SceneManager.GetSceneByBuildIndex(3);

    }


    public void PauseGame()
    {

        Debug.Log(" PauseGame() ");

        Time.timeScale = 0f;

        //AudioListener.pause = true;

    }

    public void ResumeGame()
    {

        Debug.Log(" ResumeGame() ");

        Time.timeScale = 1;

        //AudioListener.pause = false;

    }




    public void LoadExitGame()
    {

        Debug.Log(" LoadExitGame() ");

        PlayerPrefs.SetInt("NextSceneNumber", -1);

#if UNITY_EDITOR
        // Application.Quit() does not work in the editor so
        // UnityEditor.EditorApplication.isPlaying need to be set to false to end the game
        //PauseGame();

        UnityEditor.EditorApplication.isPlaying = false;

#else

        PauseGame();

        Application.Quit();

#endif

        PauseGame();

        Application.Quit();

    }

}
