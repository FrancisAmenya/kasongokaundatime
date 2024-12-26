using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Esta clase se encarga de generar constantemente obstaculos en pantalla
public class GeneradorObstaculos : MonoBehaviour
{
    private float tiempoTranscurrido, tiempoSiguienteGeneracion;

    [Header("Referencias ---")]
    public Transform puntoGeneracionCactus;
    public Transform puntoGeneracionPajaro;
    public GameObject gameObjectCactus;
    public GameObject gameObjectPajaro;

    [Header("Variables ---")]
    public float tiempoMinimoGeneracion;
    public float tiempoMaximoGeneracion;


    //El update se ejecuta una vez por cada frame
    void Update()
    {
        //Aumentamos el tiempo transcurrido cada segundo
        tiempoTranscurrido += Time.deltaTime;


        //Si el tiempo transcurrido es mayor al de la siguiente aparición, generamos un obstaculo
        if (tiempoTranscurrido >= tiempoSiguienteGeneracion)
        {
            tiempoTranscurrido = 0;
            int aux = Random.Range(0, 4); //Obtenemos un numero aleatorio para saber que obstaculo generar


            if (aux == 0) //Si es un 0 generamos un pájaro
            {
                GameObject bird = Instantiate(gameObjectPajaro, puntoGeneracionPajaro.position, transform.rotation);
                Destroy(bird, 4);
            }
            else //Si no es un 0, generamos un cactus
            {
                GameObject cactus = Instantiate(gameObjectCactus, puntoGeneracionCactus.position, transform.rotation);
                Destroy(cactus, 4);
            }


            //Cuando generamos el obstaculo obtenemos un tiempo aleatorio para la generacion del siguiente obstaculo
            tiempoSiguienteGeneracion = Random.Range(tiempoMinimoGeneracion, tiempoMaximoGeneracion);
        }
    }
}
