using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//This class controls the game state
public class GameController2 : MonoBehaviour
{
    private float parallaxSpeed = 0.2f; //Speed at which the background moves

    [Header("References ---")]
    public GameObject GameOverPanel;
    public GameObject faster;
    public GameObject PauseButton;
    public Material material;
    public Text scoreText;

    public AudioSource audio;
    public AudioClip mainMusic;
    public AudioClip gameOverMusic;
    public AudioClip levelUpSfx;

    [Header("Variables ---")]
    public float score;
    public float gameTime;
    public float timeToLevelUp;

    //Update is called once per frame
    void Update()
    {
        //Move the background to create movement sensation
        material.mainTextureOffset += new Vector2(parallaxSpeed, 0) * Time.deltaTime;

        //Each second we increase the game score and update the score text
        score += Time.deltaTime * 6;
        gameTime += Time.deltaTime;
        scoreText.text = score.ToString("f0");

        //If enough time passes, the game speeds up
        if(gameTime >= timeToLevelUp)
        {
            gameTime = 0;
            Time.timeScale += 0.15f;
            audio.PlayOneShot(levelUpSfx);
            faster.GetComponent<Animator>().Play("Faster");
        }
    }

    //Function that activates Game Over state
    public void ActivateGameOver()
    {
        Time.timeScale = 0;
        audio.clip = null;
        audio.PlayOneShot(gameOverMusic);
        PauseButton.SetActive(false);
        GameOverPanel.SetActive(true);
    }
}
