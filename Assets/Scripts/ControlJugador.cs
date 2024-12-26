using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Esta clase se encarga de hacer que el jugador pueda saltar y agacharse
public class ControlJugador : MonoBehaviour
{
    private bool tocandoSuelo, muerto;
    private float groundLength = 0.8f;

    [Header("Referencias ---")]
    public GameController gameController;
    public Animator animator;
    public AudioSource audio;
    public AudioClip sonidoSalto, sonidoMuerte;

    public LayerMask capaSuelo;
    public Rigidbody2D rigidBody;
    public EdgeCollider2D colliderEnPie;
    public BoxCollider2D colliderAgachado;

    [Header("Variables ---")]
    public float potenciaSalto; //Ideal: 7,1,3
    public float gravedad;
    public float multiplicadorCaida;


    //El update se ejecuta una vez por cada frame
    void Update()
    {
        if(muerto == false) //Mientras no perdamos, el jugador puede hacer acciones
        {
            modifyPhysics();
            //Comprobamos con un pequeño laser en los pies del dinosaurio si estamos tocando el suelo
            tocandoSuelo = Physics2D.Raycast(transform.position, Vector2.down, groundLength, capaSuelo);


            if (Input.GetKeyDown(KeyCode.Space) && tocandoSuelo) //Si pulsamos espacio y estamos tocando el suelo, saltamos
            {
                audio.PlayOneShot(sonidoSalto);
                Saltar();
            }


            if (Input.GetKeyDown(KeyCode.DownArrow)) //Si pulsamos abajo, el dinosaurio se agacha
            {
                animator.Play("Agachado");
                colliderEnPie.enabled = false;
                colliderAgachado.enabled = true;
            }
            else if (!Input.GetKey(KeyCode.DownArrow)) //Y si no lo estamos pulsando se pone en pié
            {
                animator.Play("Caminar");
                colliderEnPie.enabled = true;
                colliderAgachado.enabled = false;
            }


        }
    }



    //Función que se ejecuta cuando el jugador quiere saltar pulsando espacio
    private void Saltar()
    {
        //Impulsamos el cuerpo del jugador hacia arriba
        rigidBody.velocity = new Vector2(rigidBody.velocity.x, 0);
        rigidBody.AddForce(Vector2.up * potenciaSalto, ForceMode2D.Impulse);

        animator.Play("Caminar");
        colliderEnPie.enabled = true;
        colliderAgachado.enabled = false;
    }



    //Función que se encarga de dar una buena sensación al salto
    private void modifyPhysics()
    {
        if (tocandoSuelo)
        {
            rigidBody.gravityScale = 0;
        }
        else
        {
            rigidBody.gravityScale = gravedad;
            if(rigidBody.velocity.y < 0)
            {
                rigidBody.gravityScale = gravedad * multiplicadorCaida;
            }else if(rigidBody.velocity.y > 0 && !Input.GetKey(KeyCode.Space))
            {
                rigidBody.gravityScale = gravedad * (multiplicadorCaida / 2);
            }
        }
    }

    //Función que se ejecuta cuando el jugador choca con algo
    private void OnCollisionEnter2D(Collision2D other)
    {
        //Si el jugador choca con algo, se para el juego y salta el Game Over
        if (other.gameObject.CompareTag("Enemy"))
        {
            muerto = true;
            audio.PlayOneShot(sonidoMuerte);
            animator.Play("Muerte");

            //Le decimos al script del Game Controller que hemos perdido
            gameController.ActivarGameOver();
        }
    }

}
