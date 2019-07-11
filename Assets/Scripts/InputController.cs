using TMPro;
using UnityEngine;

public class InputController : MonoBehaviour
{
    private bool isEnabled=true;  //Служит для отключения управления на окне результатов

    private Vector2 startTouch;
    private Vector2 swipeDelta;

    private Ball ball;

    private void OnEnable()
    {       
        GameController.OnFiveReached += Disabled;
    }
    private void OnDisable()
    {
        GameController.OnFiveReached -= Disabled;
    } 
  
    private void Start()
    {
        ball = FindObjectOfType<Ball>();
    }

    void Update()
    {
        if (isEnabled)
        {
            if (Input.touchCount > 0)
            {
                if (Input.touches[0].phase == TouchPhase.Began)
                {
                    startTouch = Input.touches[0].position;
                }
                else if (Input.touches[0].phase == TouchPhase.Ended || Input.touches[0].phase == TouchPhase.Canceled)
                {
                    swipeDelta = Input.touches[0].position - startTouch;   

                    float force = swipeDelta.magnitude;
                    swipeDelta = swipeDelta.normalized;
                    if (force > 100) //отсечь случайные касания экрана
                        ball.Move(new Vector3(swipeDelta.x, 0, swipeDelta.y), force);
                    Reset();
                }
            }

        } 
    }

    void Reset()
    {
        startTouch = swipeDelta = Vector2.zero;        
    }

    void Disabled()
    {
        isEnabled = false;
    }    
 }
