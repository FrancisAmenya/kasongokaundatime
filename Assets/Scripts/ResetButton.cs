using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//Esta clase se encarga de darle una función al botón
public class ResetButton : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            ResetGame();
    }

    //Cuando pulsamos el botón reiniciamos la escena y hacemos que todo se mueva de nuevo
    public void ResetGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1;
    }

}
