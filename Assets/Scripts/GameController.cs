using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Esta clase se encarga de controlar el estado de la partida
public class GameController : MonoBehaviour
{
    private float parallaxSpeed = 0.2f; //La velocidad a la que se mueve el fondo

    [Header("Referencias ---")]
    public GameObject GameOverPanel;
    public GameObject faster;
    public Material material;
    public Text scoreText;

    public AudioSource audio;
    public AudioClip musicaPrincipal;
    public AudioClip musicaGameOver;
    public AudioClip levelUpSfx;

    [Header("Variables ---")]
    public float puntuacion;
    public float tiempoPartida;
    public float tiempoParaSubirNivel;


    //El update se ejecuta una vez por cada frame
    void Update()
    {

        //Movemos el fondo para dar sensación de movimiento
        material.mainTextureOffset += new Vector2(parallaxSpeed, 0) * Time.deltaTime;
        

        //Cada segundo aumentamos la puntuación de partida y actualizamos el texto de puntuación
        puntuacion += Time.deltaTime * 6;
        tiempoPartida += Time.deltaTime;
        scoreText.text = puntuacion.ToString("f0");


        //Si pasa el suficiente tiempo, el juego acelera 
        if(tiempoPartida >= tiempoParaSubirNivel)
        {
            tiempoPartida = 0;
            Time.timeScale += 0.15f;
            audio.PlayOneShot(levelUpSfx);
            faster.GetComponent<Animator>().Play("Faster");
        }

    }


    //Función que activa el estado de Game Over
    public void ActivarGameOver()
    {
        Time.timeScale = 0;
        audio.clip = null;
        audio.PlayOneShot(musicaGameOver);
        GameOverPanel.SetActive(true);
    }

}
