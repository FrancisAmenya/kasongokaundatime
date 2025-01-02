using UnityEngine;

public class TouchManager : MonoBehaviour
{
    public ControlJugador controlJugador;

    private Vector2 touchStart;
    private float minSwipeDistance = 50f;
    private float swipeTime = 0f;
    private float maxSwipeTime = 0.5f;

    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    touchStart = touch.position;
                    swipeTime = 0f;
                    break;

                case TouchPhase.Moved:
                    swipeTime += Time.deltaTime;
                    break;

                case TouchPhase.Ended:
                    if (swipeTime <= maxSwipeTime)
                    {
                        Vector2 swipeDelta = touch.position - touchStart;

                        if (swipeDelta.magnitude >= minSwipeDistance)
                        {
                            float swipeDirection = Mathf.Abs(swipeDelta.x) > Mathf.Abs(swipeDelta.y) 
                                ? swipeDelta.x 
                                : swipeDelta.y;

                            if (swipeDirection > 0 && Mathf.Abs(swipeDelta.y) > Mathf.Abs(swipeDelta.x))
                            {
                                Jump();
                            }
                            else if (swipeDirection < 0 && Mathf.Abs(swipeDelta.y) > Mathf.Abs(swipeDelta.x))
                            {
                                Crouch();
                            }
                        }
                    }
                    break;
            }
        }
    }

    private void Jump()
    {
        // Add your jump logic here
        controlJugador.Jump();
        Debug.Log("Jump action triggered!");
    }

    private void Crouch()
    {
        // Add your crouch logic here
        controlJugador.Crouch();
        Debug.Log("Crouch action triggered!");
    }
}
